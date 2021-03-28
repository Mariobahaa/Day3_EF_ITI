using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP.Entities;

using DP.EntityManagers;
using Microsoft.Data.SqlClient;
using Dapper;

namespace DP.EnitiyManagers
{
    public class TitleManager: IManager<title>
    {
        IDbConnection Conn = new SqlConnection("Data Source=.; Initial Catalog=pubs; Integrated Security=true;");
        public bool Add(title item)
        {
            var Affected = Conn.Execute("insertTitles  @a,@b,@c,@d,@e,@f,@g,@h,@i,@j", param: new {
                a = item.Title_id,
                b = item.Title,
                c = item.Type,
                d = item.Pub_id,
                e = item.Price,
                f = item.Advance,
                g = item.Royalty,
                h = item.Ytd_sales,
                i = item.Notes,
                j = item.Pubdate
            });

            if (Affected > 0)
                return true;
            return false;
        }

        public bool Delete(string ID)
        {
            var Affected = Conn.Execute("deleteTitles @a", param: new
            {
                a = ID   
            });
            if (Affected > 0)
                return true;
            return false;
        }

        public List<title> GetAll()
        {
            List<title> tlist = Conn.Query<title>("selectAllTitles", commandType: CommandType.StoredProcedure).ToList();
            foreach (title t in tlist) t.State = EntityState.Unchanged;
            return tlist;
        }

        //public title GetByID(long ID)
        //{
        //    throw new NotImplementedException();
        //}


 


        public bool Update(title item)
        {
            var Affected = Conn.Execute("updateTitles @a, @b, @c, @d, @e, @f, @g, @h, @i, @j",
            param: new
            {
                a = item.Title_id,
                b = item.Title,
                c = item.Type,
                d = item.Pub_id,
                e = item.Price,
                f = item.Advance,
                g = item.Royalty,
                h = item.Ytd_sales,
                i = item.Notes,
                j = item.Pubdate
            });

            if (Affected > 0)
                return true;
            return false;
        }
    }
}

/*
     public titleList DTtoTitleList(DataTable DT)
     {
         titleList titles = new titleList();
         foreach(DataRow dr in DT.Rows)
         {  
             titles.Add(DRtoTitle(dr));
         }
         return titles;
     }

     public title DRtoTitle(DataRow dr)
     {
         title t = new title();
         t.Title_id = dr["title_id"]?.ToString()??"NA";
         t.Title = dr["title"]?.ToString() ?? "NA";
         t.Type = dr["type"]?.ToString() ?? "NA";
         t.Pub_id = dr["pub_id"]?.ToString() ?? "NA";
         decimal d;
         if (decimal.TryParse(dr["price"]?.ToString()??"0.0",out d))
             t.Price = d;
         if (decimal.TryParse(dr["advance"]?.ToString() ?? "0.0", out d))
             t.Advance = d;
         int i;
         if (int.TryParse(dr["royalty"]?.ToString() ?? "0", out i))
             t.Royalty = i;
         if (int.TryParse(dr["ytd_sales"]?.ToString() ?? "0", out i))
             t.Ytd_sales = i;
         t.Notes = dr["notes"]?.ToString() ?? "NA";
         DateTime dt;
         if (DateTime.TryParse(dr["pubdate"]?.ToString() ?? "NA", out dt))
             t.Pubdate = dt;

         t.State = EntityState.Unchanged;
         return t;
     }
     public titleList selectAllTitles()
     {
         return DTtoTitleList(dbManager.ExecuteDataTable("selectAllTitles"));
     }

     public bool insertTitles(String title_id, String Title, String type, String? pub_id, decimal? price, 
         decimal? advance, int? royalty, int? ytd_sales, string notes, DateTime? pdate)
     {
         Dictionary<string, Object> ParamList = new Dictionary<string, object>();
         ParamList["a"]= title_id;
         ParamList["b"] = Title;
         ParamList["c"] = type;
         ParamList["d"] = pub_id;
         ParamList["e"] = price;
         ParamList["f"] = advance;
         ParamList["g"] = royalty;
         ParamList["h"] = ytd_sales;
         ParamList["i"] = notes;
         ParamList["j"] = pdate;


         if (dbManager.ExecuteNonQuery("insertTitles", ParamList) > 0)
             return true;
         return false;
     }


     public bool updateTitles(String title_id, String Title, String type, String? pub_id, decimal? price,
         decimal? advance, int? royalty, int? ytd_sales, string notes, DateTime? pdate)
     {
         Dictionary<string, Object> ParamList = new Dictionary<string, object>();
         ParamList["a"] = title_id;
         ParamList["b"] = Title;
         ParamList["c"] = type;
         ParamList["d"] = pub_id;
         ParamList["e"] = price;
         ParamList["f"] = advance??0;
         ParamList["g"] = royalty??0;
         ParamList["h"] = ytd_sales??0;
         ParamList["i"] = notes;
         ParamList["j"] = pdate;
         //ParamList["a"] = title_id ?? "NA";
         //ParamList["b"] = Title ?? "NA";
         //ParamList["c"] = type ?? "NA";
         //ParamList["d"] = pub_id ?? "NA";
         //ParamList["e"] = price ?? 0;
         //ParamList["f"] = advance ?? 0;
         //ParamList["g"] = royalty ?? 0;
         //ParamList["h"] = ytd_sales ?? 0;
         //ParamList["i"] = notes ?? "NA";
         //ParamList["j"] = pdate ?? DateTime.Now;


         if (dbManager.ExecuteNonQuery("updateTitles", ParamList) > 0)
             return true;
         return false;
     }

     public bool deleteTitles(String title_id)
     {
         Dictionary<string, Object> ParamList = new Dictionary<string, object>();
         ParamList["a"] = title_id;

         if (dbManager.ExecuteNonQuery("deleteTitles", ParamList) > 0)
             return true;
         return false;
     }
     */
