using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Response
{
    public class Page<T>
    {

        /**
         * current pageNum number
         */
        public int pageNum { get; set; } = 1;

        /**
         * How many pages per pageNum
         */
        public int limit { get; set; } = 10;

        /**
         * prev pageNum number
         */
        public int prevPage { get; set; } = 1;

        /**
         * next pageNum number
         */
        public int nextPage { get; set; } = 1;

        /**
         * total pageNum count
         */
        public int totalPages { get; set; } = 1;

        /**
         * total row count
         */
        public long totalRows { get; set; }

        /**
         * row list
         */
        public List<T> rows { get; set; }

        /**
         * is first pageNum
         */
        public bool isFirstPage { get; set; }

        /**
         * is last pageNum
         */
        public bool isLastPage { get; set; }

        /**
         * has prev pageNum
         */
        public bool hasPrevPage { get; set; }

        /**
         * has next pageNum
         */
        public bool hasNextPage { get; set; }

        /**
         * navigation pageNum number
         */
        public int navPages { get; set; } = 8;

        /**
         * all navigation pageNum number
         */
        public int[] navPageNums { get; set; }

        public Page<T> NavPages(int navPages)
        {
            // calculation of navigation pageNum after basic parameter setting
            this.calcNavigatePageNumbers(navPages);
            return this;
        }

        public Page()
        {
        }

        public Page(long total, int page, int limit)
        {
            init(total, page, limit);
        }

        public void init(long total, int pageNum, int limit)
        {
            // set basic params
            this.totalRows = total;
            this.limit = limit;
            this.totalPages = (int)((this.totalRows - 1) / this.limit + 1);

            // automatic correction based on the current number of the wrong input
            if (pageNum < 1)
            {
                this.pageNum = 1;
            }
            else if (pageNum > this.totalPages)
            {
                this.pageNum = this.totalPages;
            }
            else
            {
                this.pageNum = pageNum;
            }

            // calculation of navigation pageNum after basic parameter setting
            this.calcNavigatePageNumbers(this.navPages);

            // and the determination of pageNum boundaries
            judgePageBoudary();
        }

        public void calcNavigatePageNumbers(int navPages)
        {
            // when the total number of pages is less than or equal to the number of navigation pages
            if (this.totalPages <= navPages)
            {
                navPageNums = new int[totalPages];
                for (int i = 0; i < totalPages; i++)
                {
                    navPageNums[i] = i + 1;
                }
            }
            else
            {
                // when the total number of pages is greater than the number of navigation pages
                navPageNums = new int[navPages];
                int startNum = pageNum - navPages / 2;
                int endNum = pageNum + navPages / 2;
                if (startNum < 1)
                {
                    startNum = 1;
                    for (int i = 0; i < navPages; i++)
                    {
                        navPageNums[i] = startNum++;
                    }
                }
                else if (endNum > totalPages)
                {
                    endNum = totalPages;
                    for (int i = navPages - 1; i >= 0; i--)
                    {
                        navPageNums[i] = endNum--;
                    }
                }
                else
                {
                    for (int i = 0; i < navPages; i++)
                    {
                        navPageNums[i] = startNum++;
                    }
                }
            }
        }

        public void judgePageBoudary()
        {
            isFirstPage = pageNum == 1;
            isLastPage = pageNum == totalPages && pageNum != 1;
            hasPrevPage = pageNum != 1;
            hasNextPage = pageNum != totalPages;
            if (hasNextPage)
            {
                nextPage = pageNum + 1;
            }
            if (hasPrevPage)
            {
                prevPage = pageNum - 1;
            }
        }

    }
}
