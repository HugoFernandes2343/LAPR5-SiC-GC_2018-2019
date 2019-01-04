% Bibliotecas
:- use_module(library(http/thread_httpd)).
:- use_module(library(http/http_dispatch)).
:- use_module(library(http/http_parameters)).
:- use_module(library(http/http_cors)).
:- set_setting(http:cors, [*]).
:- use_module(library(http/json)).
:- use_module(library(http/http_open)).

:-dynamic(known/2).
:-dynamic(city/4).
:-dynamic(factory/3).

% Relação entre pedidos HTTP e predicados que os processam
:- http_handler('/', test, []).
:- http_handler('/add_factory', add_factory, []).
:- http_handler('/remove_factory', remove_factory, []).
:- http_handler('/list_cities', list_cities, []).
:- http_handler('/list_factories', list_factories, []).
:- http_handler('/closest_factory', closest_factory, []).
:- http_handler('/shorter_path', shorter_path, []).
:- http_handler('/copy_factory_db', loadFactories, []).


load_cities_url("https://localhost:5001/api/city").
load_factories_url("https://localhost:5001/api/factory").



%Ligar o server
server_on:- loadCities, http_server(http_dispatch, [port(3500)]),!.

server_off:-http_stop_server(3500, []).



%---------

get_data(Data, Op) :-
    (   (Op = 0, load_cities_url(URL));
        (Op = 1, load_factories_url(URL))),
    setup_call_cleanup(
        http_open(URL, In, [ request_header('Accept'='application/json'),
                             cert_verify_hook(cert_accept_any)
                           ]),
        (   set_stream(In, encoding(utf8)),
            json_read_dict(In, Data,[value_string_as(string)])
        ),
        close(In)).

parse_data_cities([]).
parse_data_cities([H|T]):-
    Id = H.get(cityId),
    Name = H.get(name),
    Lat = H.get(latitude),
    Long = H.get(longitude),
    (   city(Name,Id,_,_);
        assert(city(Name, Id, Lat, Long))),
    !, parse_data_cities(T).

parse_data_factories([]).
parse_data_factories([H|T]):-
    Id = H.get(factoryId),
    Description = H.get(description),
    City = H.get(city),
    Name = City.get(name),
    assert(factory(Name,Id,Description)),
    !, parse_data_factories(T).


loadCities:-get_data(Data, 0), parse_data_cities(Data).

loadFactories(_Request):- cors_enable,
    get_data(Data, 1),
    forall(factory(_,_,_), retract(factory(_,_,_))),
    parse_data_factories(Data),
    format('Content-type: application/json~n~n'),
    json_write(current_output, 'Success').


test(_Request):-
    format('Content-type: text/plain~n~n'),
    write('Server is up!').


%--not used
%Adiciona fabrica
%formato do GET para adicionar fabrica à KB:
%http://localhost:3500/add_factory?city=porto
add_factory(Request) :- cors_enable,
    http_parameters(Request,
                    [ city(City, [])
                    ]),
    regista_fabrica(City).
    %assert(teste(City)).
regista_fabrica(Cidade):-
    format('Content-type: application/json~n~n'),
    (   \+city(Cidade,_,_,_), json_write(current_output,'City does not exist!'),!, true);
    (   factory(Cidade,_,_), json_write(current_output,'Factory already exists!'), !, true);
    assert(factory(Cidade,_,_)),
    json_write(current_output, 'Factory registered!').
%apaga fabrica
%formato do get para apagar a fabrica da KB:
%http://localhost:3500/remove_factory?city=porto
remove_factory(Request) :- cors_enable,
    http_parameters(Request,
                    [ city(City, [])
                    ]),
    apaga_fabrica(City).
apaga_fabrica(City):-
    format('Content-type: text/plain~n~n'),
    (   \+city(City,_,_,_), json_write(current_output, 'City does not exist!'), !, true);
    (   \+factory(City,_,_), json_write(current_output, 'Factory does not exist!'), !, true);
    retract(factory(City,_,_)),
    json_write(current_output,'Factory was deleted!').
%--


%lista cidades
%formato do get para listar as cidades da KB:
%http://localhost:3500/list_cities
list_cities(_Request):- cors_enable,
    format('Content-type: application/json~n~n'),
    findall(X, city(X,_,_,_), L),
    json_write(current_output, L).

%lista fabricas
%formato do get para listar as fabricas da KB:
%http://localhost:3500/list_factories
list_factories(_Request):- cors_enable,
    format('Content-type: application/json~n~n'),
    findall(X, factory(X,_,_), L),
    json_write(current_output,L).



%fabrica mais proxima
%formato do get para receber a fabrica mais proxima da cidade indicada:
%http://localhost:3500/closest_factory?city=porto
closest_factory(Request) :-cors_enable,
    http_parameters(Request,
                    [ city(City, [string])
                    ]),
    fabrica_mais_proxima(City).
fabrica_mais_proxima(City):-
    format('Content-type: application/json~n~n'),
    (   \+city(City,_,_,_), json_write(current_output,'City does not exist!'), !, true);
    escolhe_fabrica(City, Res),
    city(Res, IdC, Lat, Long),
    factory(Res, IdF, Desc),
    json_write(current_output,json([factoryID=IdF, description=Desc, city=json([cityId=IdC, name=Res, latitude=Lat, longitude=Long])])).


escolhe_fabrica(City, Fabrica):-
    findall(X, factory(X,_,_), Fabricas),
    findall(X,( city(X,_,_,_), member(X, Fabricas) ), L),
    cidMaisProxima(City, L, Fabrica,_).

%caminho mais curto
% formato do get para receber o cmainho mais curto tendo como origem e
% fim a cidade indicada
%/shorter_path?city=porto
shorter_path(Request) :-cors_enable,
    http_parameters(Request,
                    [ city(City, [string])
                    ]),
    shorter_path1(City).

shorter_path1(City):-
    format('Content-type: application/json~n~n'),
    (   \+city(City,_,_,_), format('City does not exist!~n'), !, true);
    menor_caminho_opt(City, Lista, _),
    %menor_caminho(City, Lista, Dist),
    to_id(Lista, IDs),
    %format('~w~n',[IDs]).
    json_write(current_output, IDs).

to_id([],[]).
to_id([H|T],L):-to_id(T, L1), city(H, Id,_,_), append([Id],L1,L).


%-----------------------------------------------------------------
%calculos caminho menor


%calcula o custo de um caminho
calcCusto([_|[]],0).
calcCusto([H,S|T],Valor):- calcCusto([S|T],Valor1), dist_cities(H,S, Dist), Valor is Valor1 + Dist.

%  dist_cities(brussels,prague,D).
%  D = 716837.
dist_cities(C1,C2,Dist):-
    city(C1,_,Lat1,Lon1),
    city(C2,_,Lat2,Lon2),
    distance(Lat1,Lon1,Lat2,Lon2,Dist).

degrees2radians(Deg,Rad):-
	Rad is Deg*0.0174532925.

% distance(latitude_first_point,longitude_first_point,latitude_second_point,longitude_second_point,distance
% in meters)
distance(Lat1, Lon1, Lat2, Lon2, Dis2):-
	degrees2radians(Lat1,Psi1),
	degrees2radians(Lat2,Psi2),
	DifLat is Lat2-Lat1,
	DifLon is Lon2-Lon1,
	degrees2radians(DifLat,DeltaPsi),
	degrees2radians(DifLon,DeltaLambda),
	A is sin(DeltaPsi/2)*sin(DeltaPsi/2)+ cos(Psi1)*cos(Psi2)*sin(DeltaLambda/2)*sin(DeltaLambda/2),
	C is 2*atan2(sqrt(A), sqrt(1-A)),
	Dis1 is 6371000*C,
	Dis2 is round(Dis1).


%encontra numa lista a cidade mais proxima da indicada
cidMaisProxima(_,[],_,-1).
cidMaisProxima(Cid,[H|T],Res,Dist):- cidMaisProxima(Cid,T,Res1,Dist1), dist_cities(Cid,H,D),
    ((D < Dist1; Dist1 = -1) ->
    (   Dist = D, Res =H);
    (   Dist = Dist1, Res = Res1)).


caminhoMaisProx(_,[],[]).
caminhoMaisProx(Cid,CidadesAVisitar,Caminho):-
    cidMaisProxima(Cid,CidadesAVisitar,Cid1,_),
    findall(X,(member(X,CidadesAVisitar), X \== Cid1),Lista),
    caminhoMaisProx(Cid1,Lista,Caminho1), append([Cid1],Caminho1,Caminho).


menor_caminho(Origem, ListaCaminhos, Dist):-
    findall(X, ( city(X,_,_,_), X \= Origem), ListaCidades),
    caminhoMaisProx(Origem,ListaCidades,CaminhoInc),
    append([Origem],CaminhoInc,L1),
    append(L1,[Origem],ListaCaminhos),
    calcCusto(ListaCaminhos,Dist),!.




% Given three colinear points p, q, r, the function checks if
% point q lies on line segment 'pr'
%onSegment(P, Q, R)
onSegment((PX,PY), (QX,QY), (RX,RY)):-
    QX =< max(PX,RX),
    QX >= min(PX,RX),
    QY =< max(PY,RY),
    QY >= min(PY,RY).

% To find orientation of ordered triplet (p, q, r).
% The function returns following values
% 0 --> p, q and r are colinear
% 1 --> Clockwise
% 2 --> Counterclockwise
orientation((PX,PY), (QX,QY), (RX,RY), Orientation):-
	Val is (QY - PY) * (RX - QX) - (QX - PX) * (RY - QY),
	(Val == 0, !, Orientation is 0;
	Val >0, !, Orientation is 1;
        Orientation is 2).
orientation4cases(P1,Q1,P2,Q2,O1,O2,O3,O4):-
    orientation(P1, Q1, P2,O1),
    orientation(P1, Q1, Q2,O2),
    orientation(P2, Q2, P1,O3),
    orientation(P2, Q2, Q1,O4).

% The main function that returns true if line segment 'p1q1'
% and 'p2q2' intersect.
doIntersect(P1,Q1,P2,Q2):-
    % Find the four orientations needed for general and
    % special cases
	orientation4cases(P1,Q1,P2,Q2,O1,O2,O3,O4),
	(
    % General case
    O1 \== O2 , O3 \== O4,!;
    % Special Cases
    % p1, q1 and p2 are colinear and p2 lies on segment p1q1
    O1 == 0, onSegment(P1, P2, Q1),!;
    % p1, q1 and p2 are colinear and q2 lies on segment p1q1
    O2 == 0, onSegment(P1, Q2, Q1),!;
    % p2, q2 and p1 are colinear and p1 lies on segment p2q2
    O3 == 0, onSegment(P2, P1, Q2),!;
     % p2, q2 and q1 are colinear and q1 lies on segment p2q2
    O4 == 0, onSegment(P2, Q1, Q2),!).


%----------------------------------------------------------------------------------------------------
% rGraph(Origin,UnorderedListOfEdges,OrderedListOfEdges)
%
% Examples:
% ---------
% ?- rGraph(a,[[a,b],[b,c],[c,d],[e,f],[d,f],[e,a]],R).
%
% ?- rGraph(brussels,[[vienna, sarajevo], [sarajevo, tirana],[tirana,sofia], [sofia, minsk], [andorra,brussels],[brussels,minsk],[vienna,andorra]],R).
%
rGraph(Orig,[[Orig,Z]|R],R2):-!,
	reorderGraph([[Orig,Z]|R],R2).
rGraph(Orig,R,R3):-
	member([Orig,X],R),!,
	delete(R,[Orig,X],R2),
	reorderGraph([[Orig,X]|R2],R3).
rGraph(Orig,R,R3):-
	member([X,Orig],R),
	delete(R,[X,Orig],R2),
	reorderGraph([[Orig,X]|R2],R3).


reorderGraph([],[]).
reorderGraph([[X,Y],[Y,Z]|R],[[X,Y]|R1]):-
	reorderGraph([[Y,Z]|R],R1).
reorderGraph([[X,Y],[Z,W]|R],[[X,Y]|R2]):-
	Y\=Z,
	reorderGraph2(Y,[[Z,W]|R],R2).
reorderGraph2(_,[],[]).
reorderGraph2(Y,R1,[[Y,Z]|R2]):-
	member([Y,Z],R1),!,
	delete(R1,[Y,Z],R11),
	reorderGraph2(Z,R11,R2).
reorderGraph2(Y,R1,[[Y,Z]|R2]):-
	member([Z,Y],R1),
	delete(R1,[Z,Y],R11),
	reorderGraph2(Z,R11,R2).


%[a,b,c,d,a] -> [[a,b],[b,c],[c,d],[d,a]]
converteSegmentos([_|[]], []).
converteSegmentos([H,S|T], Res):-converteSegmentos([S|T],Res1),append([[H,S]],Res1,Res).
converteCaminhos([[H,T]],[H,T]).
converteCaminhos([[H1|_]|T],Res):-converteCaminhos(T,Res1), append([H1],Res1,Res).

cruzaVerif([H,T],L):-(member(H,L);member(T,L)).

cruza([Seg1a,Seg1b],[Seg2a,Seg2b]):-
    city(Seg1a,_,Seg1ax,Seg1ay),
    city(Seg1b,_,Seg1bx,Seg1by),
    city(Seg2a,_,Seg2ax,Seg2ay),
    city(Seg2b,_,Seg2bx,Seg2by),
    (doIntersect((Seg1ax,Seg1ay),(Seg1bx,Seg1by),(Seg2ax,Seg2ay),(Seg2bx,Seg2by)) ->
    (   cruzaVerif([Seg1a,Seg1b],[Seg2a,Seg2b]) -> (!,false);(!,true));
    (   !,false)).

% verifica se um segmento cruza com algum segmento de uma lista, retorna
% false se nao cruzar e true se cruzar
verif(_,[],[]):-!,false.
verif(Seg,[H|T],Res):-
    (cruza(Seg,H) ->
    (   !,Res = [Seg,H],true);
    (   verif(Seg,T,Res))).
% verifica todos os segmentos de uma lista, os primeiros segmentos
% cruzados que encontrar sao guardados
verificaCaminho([],_):-!,false.
verificaCaminho([H,S|T],Res):-
    (verif(H,[S|T],Res1) ->
    (   !,Res = Res1,true);
    (   verificaCaminho([S|T],Res))).

%verifica se o segmento dado exista na lista Excl
naoMembro([H,T],Excl):-(\+member([H,T],Excl), \+member([T,H],Excl)).

% resolve cruzamentos, começa por pegar na primeira cidade do segmento
% A, verifica qual das cidades do Segmento B é mais proxima e gera 1 de
% 2 combinacoes possiveis
% De seguida verifica se o par de segmentos gerado nao existe no
% caminho, usando a 2 combinaçao possivel se ja existir
resolveCruzamentos([[H1,T1],[H2,T2]],Excl,Res):-
    dist_cities(H1,H2,C1),
    dist_cities(H1,T2,C2),
    (C1<C2 ->
    (   (naoMembro([H1,H2],Excl), naoMembro([T1,T2],Excl)) ->
        (   Res=[[H1,H2],[T1,T2]]);
        (   Res=[[H1,T2],[H2,T1]]));
    (   (naoMembro([H1,T2],Excl), naoMembro([H2,T1],Excl)) ->
        (   Res=[[H1,T2],[H2,T1]]);
        (   Res=[[H1,H2],[T1,T2]]))).

% percorre o caminho verificando se existe cruzamentos, caso exista
% resolve com o predicado acima e chama-se a si mesmo outra vez com o
% novo caminho
constroi(Cam,Res):-
    (verificaCaminho(Cam,Seg) ->
    (   resolveCruzamentos(Seg,Cam,Resolv), findall(X,(member(X,Cam),\+member(X,Seg)),PathInc), append(PathInc,Resolv,Res));
    (   !,Res=Cam,true)).


menor_caminho_opt(Cid,Cam,Dist):- menor_caminho(Cid,Path1,_),
    converteSegmentos(Path1,Path),
    constroi(Path,Res),
    (rGraph(Cid,Res,Cam1) ->
    (   converteCaminhos(Cam1,Cam));
    (   Cam1 = Res, converteCaminhos(Cam1,Cam))),
    calcCusto(Cam,Dist),!.









