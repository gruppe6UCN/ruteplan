using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace WebApp
{
    public partial class RouteDetil : System.Web.UI.Page
    {
        protected long id;
        protected WCFClient wcfClient = WCFClient.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            object _id = RouteData.Values["route_id"];
            if (_id != null)
            {
                bool parsed = Int64.TryParse(_id.ToString(), out id);

                if (parsed && wcfClient.Routes.ContainsKey(id))
                {
                    Table1.CellPadding = 1;
                    Table1.CellSpacing = 0;
                    Table1.Rows.Add(new TableRow
                    {
                        Cells =
                        {
                            new TableCell {Text = "ID", BorderStyle = BorderStyle.Solid, BorderColor = Color.Black, BorderWidth = new Unit("2px")},
                            new TableCell {Text = "Customer ID", BorderStyle = BorderStyle.Solid, BorderColor = Color.Black, BorderWidth = new Unit("2px")},
                            new TableCell {Text = "Address", BorderStyle = BorderStyle.Solid, BorderColor = Color.Black, BorderWidth = new Unit("2px")},
                            new TableCell {Text = "Amount", BorderStyle = BorderStyle.Solid, BorderColor = Color.Black, BorderWidth = new Unit("2px")},
                        },
                        BackColor = Color.Black,
                        ForeColor = Color.White,
                        Font = {Bold = true},
                    });

                    foreach (DeliveryStop stop in wcfClient.Routes[id].Stops)
                    {
                        Table1.Rows.Add(new TableRow
                        {
                            Cells =
                            {
                                new TableCell {Text = stop.ID != 0 ? stop.ID.ToString() : stop.DefaultStop.ID.ToString(), BorderStyle = BorderStyle.Solid, BorderColor = Color.LightGray, BorderWidth = new Unit("2px")},
                                new TableCell {Text = "", BorderStyle = BorderStyle.Solid, BorderColor = Color.LightGray, BorderWidth = new Unit("2px")},
                                new TableCell {Text = "", BorderStyle = BorderStyle.Solid, BorderColor = Color.LightGray, BorderWidth = new Unit("2px")},
                                new TableCell {Text = stop.GetSizeOfTransportUnits().ToString(), BorderStyle = BorderStyle.Solid, BorderColor = Color.LightGray, BorderWidth = new Unit("2px")},
                            },
                            BackColor = Color.LightGray,
                        });

                        foreach (Customer customer in stop.DefaultStop.Customers)
                        {
                            double amount = 0.0;
                            foreach (TransportUnit transportUnit in stop.TransportUnits)
                            {
                                if (transportUnit.CustomerID == customer.ID)
                                {
                                    amount += transportUnit.UnitType;
                                }
                            }

                            Table1.Rows.Add(new TableRow
                            {
                                Cells =
                                {
                                    new TableCell {Text = "", BorderStyle = BorderStyle.Solid, BorderColor = Color.White, BorderWidth = new Unit("2px")},
                                    new TableCell {Text = customer.ID.ToString(), BorderStyle = BorderStyle.Solid, BorderColor = Color.White, BorderWidth = new Unit("2px")},
                                    new TableCell {Text =  String.Format("{0} {1}, {2} {3}", customer.StreetName, customer.StreetNo, customer.Zipcode, customer.City), BorderStyle = BorderStyle.Solid, BorderColor = Color.White, BorderWidth = new Unit("2px")},
                                    new TableCell {Text = amount.ToString(), BorderStyle = BorderStyle.Solid, BorderColor = Color.White, BorderWidth = new Unit("2px")},
                                }
                            });
                        }
                    }
                }
            }
        }

        protected string GetTitle()
        {
            object _id = RouteData.Values["route_id"];
            if (_id != null)
            {
                if (id != 0)
                {
                    return String.Format("This RouteDetail is {0}", id);
                }
                return String.Format("this route '{0}' does not exist", _id);
            }
            return String.Format("<a href=''>Please specify a route</a>");
        }
    }
}