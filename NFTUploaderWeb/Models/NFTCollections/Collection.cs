using System.ComponentModel.DataAnnotations.Schema;

namespace NFTUploaderWeb.Models.NFTCollections
{
    public class Collection
    {
        public string Contract { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public CollectionData CollectionData { get; set; }
    }
}