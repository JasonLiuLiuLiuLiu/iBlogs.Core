using System.Collections.Generic;

namespace iBlogs.Site.Core.Common.DTO
{
    public class Page<T>
    {

        /**
         * current pageNum number
         */
        public int PageNum { get; set; } = 1;

        /**
         * How many pages per pageNum
         */
        public int Limit { get; set; } = 10;

        /**
         * prev pageNum number
         */
        public int PrevPage { get; set; } = 1;

        /**
         * next pageNum number
         */
        public int NextPage { get; set; } = 1;

        /**
         * total pageNum count
         */
        public int TotalPages { get; set; } = 1;

        /**
         * total row count
         */
        public long TotalRows { get; set; }

        /**
         * row list
         */
        public List<T> Rows { get; set; }

        /**
         * is first pageNum
         */
        public bool IsFirstPage { get; set; }

        /**
         * is last pageNum
         */
        public bool IsLastPage { get; set; }

        /**
         * has prev pageNum
         */
        public bool HasPrevPage { get; set; }

        /**
         * has next pageNum
         */
        public bool HasNextPage { get; set; }

        /**
         * navigation pageNum number
         */
        public int NavPages { get; set; } = 8;

        /**
         * all navigation pageNum number
         */
        public int[] NavPageNums { get; set; }=new []{1};

        public Page<T> SetNavPages(int navPages)
        {
            // calculation of navigation pageNum after basic parameter setting
            CalcNavigatePageNumbers(navPages);
            return this;
        }

        public Page()
        {
            Rows=new List<T>();
        }

        public Page(long total, int page, int limit)
        {
            Init(total, page, limit);
        }

        public Page(long total, int page, int limit,List<T> rows)
        {
            Init(total, page, limit);
            Rows = rows;
        }

        public void Init(long total, int pageNum, int limit)
        {
            // set basic params
            TotalRows = total;
            Limit = limit;
            TotalPages = (int)((TotalRows - 1) / Limit + 1);

            // automatic correction based on the current number of the wrong input
            if (pageNum < 1)
            {
                PageNum = 1;
            }
            else if (pageNum > TotalPages)
            {
                PageNum = TotalPages;
            }
            else
            {
                PageNum = pageNum;
            }

            // calculation of navigation pageNum after basic parameter setting
            CalcNavigatePageNumbers(NavPages);

            // and the determination of pageNum boundaries
            JudgePageBoudary();
        }

        public void CalcNavigatePageNumbers(int navPages)
        {
            // when the total number of pages is less than or equal to the number of navigation pages
            if (TotalPages <= navPages)
            {
                NavPageNums = new int[TotalPages];
                for (int i = 0; i < TotalPages; i++)
                {
                    NavPageNums[i] = i + 1;
                }
            }
            else
            {
                // when the total number of pages is greater than the number of navigation pages
                NavPageNums = new int[navPages];
                int startNum = PageNum - navPages / 2;
                int endNum = PageNum + navPages / 2;
                if (startNum < 1)
                {
                    startNum = 1;
                    for (int i = 0; i < navPages; i++)
                    {
                        NavPageNums[i] = startNum++;
                    }
                }
                else if (endNum > TotalPages)
                {
                    endNum = TotalPages;
                    for (int i = navPages - 1; i >= 0; i--)
                    {
                        NavPageNums[i] = endNum--;
                    }
                }
                else
                {
                    for (int i = 0; i < navPages; i++)
                    {
                        NavPageNums[i] = startNum++;
                    }
                }
            }
        }

        public void JudgePageBoudary()
        {
            IsFirstPage = PageNum == 1;
            IsLastPage = PageNum == TotalPages && PageNum != 1;
            HasPrevPage = PageNum != 1;
            HasNextPage = PageNum != TotalPages;
            if (HasNextPage)
            {
                NextPage = PageNum + 1;
            }
            if (HasPrevPage)
            {
                PrevPage = PageNum - 1;
            }
        }

    }
}
