using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Otel.Model
{
    public class Map
    {
        private MySQL sql;

        long room_id;
        long book_id;
        DateTime calendar_day;

        public Map(MySQL sql)
        {
            this.sql = sql;
        }

        public void SelectMap(long root_id, long book_id, DateTime calendar_day)
        {
            this.room_id = room_id;
            this.book_id = book_id;
            this.calendar_day = calendar_day;
        }

    }
}
