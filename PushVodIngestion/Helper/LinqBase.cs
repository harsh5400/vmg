using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace PushVodIngestion
{
        public class LinqBase
        {
            public static myDataDataContext GetNewDataContext()
                    {
                        return new myDataDataContext(Global.ConnectionStringProject);
                    }
                              

            public static IQueryable<T>  GetQueryableList<T>(Boolean loadOption = false) where T: class
            {
                var store = GetNewDataContext();

                        //switch (typeof(T).Name.ToString())
                        //{

                        //    case "frmPdlMaster":
                        //        store.LoadOptions = PdlMasterLoadOption();
                        //        break;

                        //    case "tblTstvSchdule":
                        //        if(loadOption)
                        //            store.LoadOptions = tblTstvSchduleLoadOption();
                        //        break;
                        //    default:
                        //        break;
                        //}



                store.DeferredLoadingEnabled = loadOption;
                        return from table in store.GetTable<T>() select table;

                    }
            

            //private static DataLoadOptions PdlMasterLoadOption()
            //{
            //    DataLoadOptions loadOptions = new DataLoadOptions();
            //    loadOptions.LoadWith<tblPdlMaster>(p => p.tblCategoryLocation);
            //    loadOptions.LoadWith<tblPdlMaster>(p => p.tblContentProvider);
            //    loadOptions.LoadWith<tblPdlMaster>(p => p.tblLanguage);
            //    loadOptions.LoadWith<tblPdlMaster>(p => p.tblSource);
                
            //    return loadOptions;
            //}

            //private static DataLoadOptions tblTstvSchduleLoadOption()
            //{
            //    DataLoadOptions loadOptions = new DataLoadOptions();
            //    loadOptions.LoadWith<tblTstvSchdule>(p => p.tblTstvChannel);                
            //    return loadOptions;
            //}

            public static List<T> GetList<T>(Boolean loadOption = false) where T : class
                        {
                            return GetQueryableList<T>(loadOption).ToList<T>();
                        }

            




            public static void UpdateDatabaseWithItem<T>(T item) where T : class
            {
                myDataDataContext store = new myDataDataContext(Global.ConnectionStringProject);
                var table = store.GetTable<T>();
                table.Attach(item);
                store.Refresh(RefreshMode.KeepCurrentValues, item); //This is a hack but it works
                store.SubmitChanges();
            }
 
            public static void InsertItemIntoDatabase<T>(T item) where T : class
            {
            
                   var store = GetNewDataContext();
                var table = store.GetTable<T>();
                table.InsertOnSubmit(item);
                store.SubmitChanges();
            }
 
            public static void DeleteItemFromDatabase<T>(T item) where T : class
            {
            var store = GetNewDataContext();
            var table = store.GetTable<T>();
            table.Attach(item);
            table.DeleteOnSubmit(item);
            store.SubmitChanges();
            }
 
                public static void EmptyOrTruncateTable(string tableName, bool truncate)
                {
                var store = GetNewDataContext();
 
                if (truncate)
                {
                store.ExecuteCommand("TRUNCATE TABLE " + tableName);
                }
                else
                {
                GetNewDataContext().ExecuteCommand("DELETE FROM " + tableName);
                }
 
                store.SubmitChanges();
                }

                public static DataTable ListToDataTable<T>(List<T> list)
                {
                    DataTable dt = new DataTable();

                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    {
                        dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
                    }
                    foreach (T t in list)
                    {
                        DataRow row = dt.NewRow();
                        foreach (PropertyInfo info in typeof(T).GetProperties())
                        {

                          try 
                          {
                       if (!IsNullableType(info.PropertyType))
                              row[info.Name] = info.GetValue(t, null);
                       else
                          row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                        }
                          catch
                          {
                          }

                          
                            }
                        dt.Rows.Add(row);
                        
                    }
                    return dt;
                }

                private static Type GetNullableType(Type t)
                {
                    Type returnType = t;
                    if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        returnType = Nullable.GetUnderlyingType(t);
                    }
                    return returnType;
                }
                private static bool IsNullableType(Type type)
                {
                    return (type == typeof(string) ||
                            type.IsArray ||
                            (type.IsGenericType &&
                             type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
                }

               public static DataTable ConvertListToDataTable(List<string[]> list)
                {
                    // New table.
                    DataTable table = new DataTable();

                    // Get max columns.
                    int columns = 0;
                        foreach (var array in list)
                        {
                            if (array.Length > columns)
                            {
                                columns = array.Length;
                            }
                        }

                    // Add columns.
                    for (int i = 0; i < columns; i++)
                    {
                        table.Columns.Add();
                    }

                    // Add rows.
                    foreach (var array in list)
                    {
                        table.Rows.Add(array);
                    }

                    return table;
                }

            //http://www.c-sharpcorner.com/uploadfile/scottlysle/generic-data-access-using-linq-to-sql-and-C-Sharp/


               //public static List<LINQEntityBase> GetEntities(string entityTypeName)
               //{
               //    List<LINQEntityBase> entities;

               //    var context = GetNewDataContext();
               //    var type = Type.GetType(entityTypeName);

               //    context.DeferredLoadingEnabled = false;
               //    entities = context.GetTable(type).Cast<LINQEntityBase>().ToList();
               //    return entities;
               //}
 
        
        }



}