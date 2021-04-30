using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twatter_postservice.DTO
{
    public class SearchDTO
    {
        #region fields
        private string searchTerm;
        #endregion

        #region constructors
        //empty constructor for JSON
        public SearchDTO()
        {

        }

        public SearchDTO(string searchTerm)
        {
            SearchTerm = searchTerm;
        }


        #endregion
        #region properties
        public string SearchTerm { get => searchTerm; set => searchTerm = value; }
        #endregion

    }
}
