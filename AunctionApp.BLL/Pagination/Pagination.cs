namespace AunctionApp.BLL.Pagination
{
    public class Pagination
    {
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int StartingPage { get; set; }
        public int EndingPage { get; set; }


        public Pagination() { }
        public Pagination(int totalItems, int page, int pagesize=10) 
        { 
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pagesize);
            int currentpage = page;

            int startpage = currentpage - 5;
            int endpage = currentpage + 4;

            if (startpage <=0)
            {
                endpage = endpage - (startpage - 1);
                startpage = 1;
            }

            if(endpage > totalPages) 
            {
                endpage = totalPages;
                if(endpage > 10)
                {
                    startpage = endpage - 9;
                }
            }

            TotalItems = totalPages;
            CurrentPage = currentpage;
            PageSize = pagesize;
            StartingPage = startpage;
            EndingPage = endpage;
            TotalPages = totalPages;
        }
    }
}
