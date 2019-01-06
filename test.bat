dotnet build
del test_results.txt
echo ----------------------------------------------------Tests---------------------------------------------------->>test_results.txt
rem ******************  MAIN CODE SECTION
set STARTTIME=%TIME%
    
echo ----------------------------------------------------TestGetCatalogsSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestGetCatalogsSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCatalogsFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestGetCatalogsFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCatalogSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestGetCatalogSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCatalogFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestGetCatalogFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutCatalogSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestPutCatalogSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutCatalogFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestPutCatalogFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostCatalogSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestPostCatalogSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostCatalogFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestPostCatalogFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteCatalogSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestDeleteCatalogSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteCatalogFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestDeleteCatalogFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestAddProductSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestAddProductSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestAddProductFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestAddProductFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteProductSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestDeleteProductSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteProductFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CatalogControllerTest.TestDeleteProductFail >> test_results.txt 2>&1

echo ----------------------------------------------------TestGetCollectionsSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestGetCollectionsSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCollectionsFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestGetCollectionsFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCollectionSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestGetCollectionSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCollectionFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestGetCollectionFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutCollectionSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestPutCollectionSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutCollectionFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestPutCollectionFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostCollectionSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestPostCollectionSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostCollectionFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestPostCollectionFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteCollectionSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestDeleteCollectionSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteCollectionFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestDeleteCollectionFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestAddProductSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestAddProductSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestAddProductFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestAddProductFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteProductSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestDeleteProductSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteProductFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CollectionControllerTest.TestDeleteProductFail >> test_results.txt 2>&1

echo ----------------------------------------------------TestGetPricesSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestGetPricesSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetPricesFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestGetPricesFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetPriceSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestGetPriceSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetPriceFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestGetPriceFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutPriceSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestPutPriceSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutPriceFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestPutPriceFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostPriceSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestPostPriceSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostPriceFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestPostPriceFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeletePriceSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestDeletePriceSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeletePriceFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestDeletePriceFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetPriceByEntitySuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestGetPriceByEntitySuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetPriceByEntityFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.PriceControllerTest.TestGetPriceByEntityFail >> test_results.txt 2>&1

echo ----------------------------------------------------TestGetCitiesSuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestGetCitiesSuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCitiesFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestGetCitiesFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCitySuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestGetCitySuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestGetCityFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestGetCityFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutCitySuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestPutCitySuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPutCityFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestPutCityFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostCitySuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestPostCitySuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestPostCityFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestPostCityFail >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteCitySuccess---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestDeleteCitySuccess >> test_results.txt 2>&1
echo ----------------------------------------------------TestDeleteCityFail---------------------------------------------------->>test_results.txt
dotnet test --no-build --filter LAPR5_GC.UnitTests.CityControllerTest.TestDeleteCityFail >> test_results.txt 2>&1

echo ----------------------------------------------------Total Compilation Time---------------------------------------------------->>test_results.txt

set ENDTIME=%TIME%
rem ******************  END MAIN CODE SECTION

rem Change formatting for the start and end times

for /F "tokens=1-4 delims=:.," %%a in ("%STARTTIME%") do (
    set /A "start=(((%%a*60)+1%%b %% 100)*60+1%%c %% 100)*100+1%%d %% 100"
)

for /F "tokens=1-4 delims=:.," %%a in ("%ENDTIME%") do (
    set /A "end=(((%%a*60)+1%%b %% 100)*60+1%%c %% 100)*100+1%%d %% 100"
)

rem Calculate the elapsed time by subtracting values
set /A elapsed=end-start

rem Format the results for output
set /A hh=elapsed/(60*60*100), rest=elapsed%%(60*60*100), mm=rest/(60*100), rest%%=60*100, ss=rest/100, cc=rest%%100
if %hh% lss 10 set hh=0%hh%
if %mm% lss 10 set mm=0%mm%
if %ss% lss 10 set ss=0%ss%
if %cc% lss 10 set cc=0%cc%

set DURATION=%hh%:%mm%:%ss%,%cc%

echo Start    : %STARTTIME%>>test_results.txt
echo Finish   : %ENDTIME%>>test_results.txt
echo          -------------->>test_results.txt
echo Duration : %DURATION%>>test_results.txt
