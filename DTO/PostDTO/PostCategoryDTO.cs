namespace SiC.DTO
{
    public class PostCategoryDTO
    {
        public PostCategoryDTO()
        {
            ParentID = -1;
        }

        public string Name { get; set; }

        public long? ParentID { get; set; }
    }
}