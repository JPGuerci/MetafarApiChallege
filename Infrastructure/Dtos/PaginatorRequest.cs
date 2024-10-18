using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetafarApiChallege.Infrastructure.Dtos
{
    public class PaginatorRequest
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 10;

        public PaginatorRequest()
        {
            PageNumber = DefaultPageNumber;
            PageSize = DefaultPageSize;
        }

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0.")]
        [DefaultValue(DefaultPageNumber)]
        public int PageNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 0.")]
        [DefaultValue(DefaultPageSize)]
        public int PageSize { get; set; }
    }
}
