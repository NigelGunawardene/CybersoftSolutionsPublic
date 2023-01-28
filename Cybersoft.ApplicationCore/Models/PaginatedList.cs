using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cybersoft.ApplicationCore.Models
{
    //[JsonObject(MemberSerialization = MemberSerialization.Fields)]
    [JsonObject]
    public class PaginatedList<T> : List<T>
    {
        //[JsonProperty]
        public IEnumerable<T> Items { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
            //AddRange(items);
        }
        public static PaginatedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        public static PaginatedList<T> ToCustomPagedList(IQueryable<T> items, int totalItemCount, int pageNumber, int pageSize)
        {
            return new PaginatedList<T>(items.ToList(), totalItemCount, pageNumber, pageSize);
        }

        //public static PaginatedList<T> ToPagedList(IQueryable<T> items, int pageNumber, int pageSize, int totalCount)
        //{
        //    return new PaginatedList<T>(items.ToList(), totalCount, pageNumber, pageSize);
        //}



        //public int PageIndex { get; private set; }
        //public int TotalPages { get; private set; }

        //public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        //{
        //    PageIndex = pageIndex;
        //    TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        //    this.AddRange(items);
        //}

        //public bool HasPreviousPage => PageIndex > 1;

        //public bool HasNextPage => PageIndex < TotalPages;

        ////public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        ////{
        ////    var count = await source.CountAsync();
        ////    var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        ////    return new PaginatedList<T>(items, count, pageIndex, pageSize);
        ////}
        //public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        //{
        //    var count = source.Count();
        //    var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    return new PaginatedList<T>(items, count, pageIndex, pageSize);
        //}

    }
}
