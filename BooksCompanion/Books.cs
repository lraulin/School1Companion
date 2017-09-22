using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BooksCompanion
{
    public class Books : Dictionary<int, Book>
    {
        #region Properties
        private decimal _TotalPrice;
        private decimal _AveragePrice;

        public decimal TotalPrice
        {
            get
            {
                _TotalPrice = 0;
                foreach (int key in this.Keys)
                {
                    _TotalPrice += this[key].Price;
                }
                return this._TotalPrice;
            }
        }

        public decimal AveragePrice
        {
            get
            {
                if (this.Count() == 0)
                {
                    return 0;
                }
                else
                {
                    _AveragePrice = this.TotalPrice / this.Count();
                    return _AveragePrice;
                }
            }
        }
        #endregion

        public Books()
        {

        }

        public Books(string sCnxn, string sLogPath)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookFetchAll";

                oCnxn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();

                while (oReader.Read())
                {
                    Book oNewBook = new Book();
                    oNewBook.BookID = Convert.ToInt32(oReader["BookID"]);
                    oNewBook.BookTitle = oReader["BookTitle"].ToString();
                    oNewBook.AuthorName = oReader["AuthorName"].ToString();
                    oNewBook.Length = Convert.ToInt32(oReader["Length"]);
                    oNewBook.IsOnAmazon = Convert.ToBoolean(oReader["IsOnAmazon"]);
                    oNewBook.DateCreated = oReader["DateCreated"].ToString();
                    oNewBook.Price = Convert.ToDecimal(oReader["Price"]);
                    if (!this.ContainsKey(oNewBook.BookID))
                        this.Add(oNewBook.BookID, oNewBook);
                }
                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BooksConstructor", ex.Message, sLogPath);
            }
        }

        public Books(string sCnxn, string sLogPath, string sBookTitle)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookSearchTitle";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add(parameterName: "@Search", sqlDbType: SqlDbType.VarChar).Value = sBookTitle;

                oCnxn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();

                while (oReader.Read())
                {
                    Book oNewBook = new Book();
                    oNewBook.BookID = Convert.ToInt32(oReader["BookID"]);
                    oNewBook.BookTitle = oReader["BookTitle"].ToString();
                    oNewBook.AuthorName = oReader["AuthorName"].ToString();
                    oNewBook.Length = Convert.ToInt32(oReader["Length"]);
                    oNewBook.IsOnAmazon = Convert.ToBoolean(oReader["IsOnAmazon"]);
                    oNewBook.DateCreated = oReader["DateCreated"].ToString();
                    oNewBook.Price = Convert.ToDecimal(oReader["Price"]);
                    if (!this.ContainsKey(oNewBook.BookID))
                        this.Add(oNewBook.BookID, oNewBook);
                }
                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BooksConstructor", ex.Message, sLogPath);
            }
        }

        public DataTable BooksList(string sCnxn, string sLogPath)
        {
            try
            {
                List<Book> oBooks = new List<Book>();
                Dictionary<int, Book> oBooksNew = new Dictionary<int, Book>();

                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookFetchAll";

                DataTable dtBook = new DataTable();
                SqlDataAdapter daBook = new SqlDataAdapter();
                daBook.SelectCommand = oCmd;

                oCnxn.Open();
                daBook.SelectCommand.ExecuteNonQuery();
                daBook.Fill(dtBook);
                oCnxn.Close();

                return (dtBook);
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BookList", ex.Message, sLogPath);
                return (null);
            }
        }
    }

    public class Book
    {
        #region Properties
        private int _BookID;
        private string _BookTitle;
        private string _AuthorName;
        private int _Length;
        private bool _IsOnAmazon;
        private string _DateCreated;
        private decimal _Price;

        public int BookID
        {
            get
            {
                return this._BookID;
            }

            set
            {
                this._BookID = value;
            }
        }

        public string BookTitle
        {
            get
            {
                return this._BookTitle;
            }

            set
            {
                this._BookTitle = value;
            }
        }

        public string AuthorName
        {
            get
            {
                return this._AuthorName;
            }

            set
            {
                this._AuthorName = value;
            }
        }

        public int Length
        {
            get
            {
                return this._Length;
            }

            set
            {
                this._Length = value;
            }
        }

        public bool IsOnAmazon
        {
            get
            {
                return this._IsOnAmazon;
            }

            set
            {
                this._IsOnAmazon = value;
            }
        }

        public string DateCreated
        {
            get
            {
                return this._DateCreated;
            }

            set
            {
                this._DateCreated = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._Price;
            }

            set
            {
                this._Price = value;
            }
        }
        #endregion Properties

        public Book()
        {

        }

        public Book(string sCnxn, string sLogPath, int iBookID)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookByID";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@BookID", SqlDbType.Int).Value = iBookID;

                oCnxn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();
                while (oReader.Read())
                {
                    this.BookID = Convert.ToInt32(oReader["BookID"]);
                    this.BookTitle = oReader["BookTitle"].ToString();
                    this.AuthorName = oReader["AuthorName"].ToString();
                    this.Length = Convert.ToInt32(oReader["Length"]);
                    this.IsOnAmazon = Convert.ToBoolean(oReader["IsOnAmazon"]);
                    this.DateCreated = oReader["DateCreated"].ToString();
                    this.Price = Convert.ToDecimal(oReader["Price"]);
                }
                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BooksConstructor", ex.Message, sLogPath);
            }
        }

        public bool Save(string sCnxn, string sLogPath)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookSave";
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add("@BookTitle", SqlDbType.NVarChar, 50).Value = this._BookTitle;
                oCmd.Parameters.Add("@AuthorName", SqlDbType.NVarChar, 50).Value = this._AuthorName;
                oCmd.Parameters.Add("@Length", SqlDbType.Int).Value = this._Length;
                oCmd.Parameters.Add("@IsOnAmazon", SqlDbType.Bit).Value = this._IsOnAmazon;
                oCmd.Parameters.Add("@BookID", SqlDbType.Int).Value = this._BookID;

                oCnxn.Open();
                oCmd.ExecuteNonQuery();
                oCnxn.Close();

                return true;
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BookList", ex.Message, sLogPath);
                return false;
            }
        }
    }
}
