using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace PROGCS05_Dion.Models {
    public class Dropdowns {
        public Dropdowns() {
            // Capaciteit
            cDrop = new List<SelectListItem>();

            cDrop.Add(new SelectListItem {
                Value = "2",
                Text = "Tweepersoonskamer",
                Selected = true
            });
            cDrop.Add(new SelectListItem {
                Value = "3",
                Text = "Driepersoonskamer"
            });
            cDrop.Add(new SelectListItem {
                Value = "5",
                Text = "Vijfpersoonskamer"
            });

            // Geslacht
            sDrop = new List<SelectListItem>();

            sDrop.Add(new SelectListItem {
                Value = "man",
                Text = "man",
                Selected = true
            });
            sDrop.Add(new SelectListItem {
                Value = "vrouw",
                Text = "vrouw"
            });
        }

        // Capacity
        public List<SelectListItem> cDrop { get; set; }
        // Sex
        public List<SelectListItem> sDrop { get; set; }
    }
}