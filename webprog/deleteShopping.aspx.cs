using System;
using System.Collections.Generic;
using System.Linq;
using webprog.Domain;

namespace webprog
{
    public partial class deleteShopping : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            else if (!(Request.QueryString["m"] == null
                || Request.QueryString["tt"] == null))
            {
                int matchID = Convert.ToInt32(Request.QueryString["m"]);
                int ttID = Convert.ToInt32(Request.QueryString["tt"]);

                if (Session["shoppingCart"] != null)
                {
                    List<Ticket> cart = (List<Ticket>)Session["shoppingCart"];
                    List<int> toRemove = new List<int>();
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if (cart[i].match.id == matchID && cart[i].ticket_type.id == ttID)
                        {
                            toRemove.Add(i);
                        }
                    }
                    for (int i = 0; i < toRemove.Count; i++)
                    {
                        cart.RemoveAt(toRemove[i]);
                    }
                    Session["shoppingCart"] = cart;
                }
            }
            Response.Redirect("shoppingcart.aspx");
        }
    }
}