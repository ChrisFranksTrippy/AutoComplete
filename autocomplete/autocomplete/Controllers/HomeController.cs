using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using autocomplete.Models;

//Chris Franks

namespace autocomplete.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Home()
        {
            setSessionVarsNull();
            return View();
        }

        public void setSessionVarsNull()
        {
            Session["lastFilter"] = null;
            Session["distinctResults"] = null;

            Session["EffectiveLength"] = null;
            Session["FldRndSur"] = null; 
            Session["InstrumentType"] = null;
            Session["PartNo"] = null;
            Session["ShankClearance"] = null;
            Session["ShankLength"] = null;
            Session["StyliType"] = null;    
            Session["TipAngle"] = null;     
            Session["TipMaterial"] = null;   
            Session["TipSize"] = null;       
            Session["TipType"] = null; 
        }

        public void setSessionVar(String key, String value)
        {
            if (String.IsNullOrWhiteSpace(value))
                Session[key] = null;
            else
                Session[key] = value;
        }


        public JsonResult autoComplete(String filter, String term)
        {
            String lastFilter = (String)Session["lastFilter"];
            
            List<String> distinctResults = new List<String>();
            List<String> searchResults = new List<String>();

            if (lastFilter != filter || lastFilter == null)
            {
                List<spStyliSearch_Result> resultList;
                StyliConnection spData = new StyliConnection();
                resultList = spData.spStyliSearch(
                    (String)Session["EffectiveLength"], //EffectiveLength                        
                    (String)Session["FldRndSur"],       //FldRndSur
                    (String)Session["InstrumentType"],  //InstrumentType
                    (String)Session["PartNo"],          //PartNo
                    (String)Session["ShankClearance"],  //Shank Clearance
                    (String)Session["ShankLength"],     //ShankLength
                    (String)Session["StyliType"],       //StyliType
                    (String)Session["TipAngle"],        //TipAngle
                    (String)Session["TipMaterial"],     //TipMaterial
                    (String)Session["TipSize"],         //TipSize
                    (String)Session["TipType"])         //TipType
                    .ToList();

                switch (filter)
                {
                    case "EffectiveLength":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).EffectiveLength))
                                distinctResults.Add(resultList.ElementAt(i).EffectiveLength);

                        }
                        break;

                    case "FldRndSur":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).FldRndSur))
                                distinctResults.Add(resultList.ElementAt(i).FldRndSur);

                        }
                        break;

                    case "InstrumentType":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).InstrumentType))
                                distinctResults.Add(resultList.ElementAt(i).InstrumentType);

                        }
                        break;

                    case "PartNo":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).PartNo))
                                distinctResults.Add(resultList.ElementAt(i).PartNo);

                        }
                        break;

                    case "ShankClearance":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).ShankClearance))
                                distinctResults.Add(resultList.ElementAt(i).ShankClearance);

                        }
                        break;

                    case "ShankLength":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).ShankLength))
                                distinctResults.Add(resultList.ElementAt(i).ShankLength);

                        }
                        break;

                    case "StyliType":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).StylusType))
                                distinctResults.Add(resultList.ElementAt(i).StylusType);

                        }
                        break;

                    case "TipAngle":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).TipAngle))
                                distinctResults.Add(resultList.ElementAt(i).TipAngle);

                        }
                        break;

                    case "TipMaterial":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).TipMaterial))
                                distinctResults.Add(resultList.ElementAt(i).TipMaterial);

                        }
                        break;

                    case "TipSize":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).TipSize))
                                distinctResults.Add(resultList.ElementAt(i).TipSize);

                        }
                        break;

                    case "TipType":
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).TipType))
                                distinctResults.Add(resultList.ElementAt(i).TipType);

                        }
                        break;
                    default:
                        for (int i = 0; i < resultList.Count; i++)
                        {
                            if (!distinctResults.Contains(resultList.ElementAt(i).PartNo))
                                distinctResults.Add(resultList.ElementAt(i).PartNo);
                        }
                        break;
                }

                List<String> nullList = new List<String>();

                foreach (String nullCheck in distinctResults)
                {
                    if (nullCheck == null || nullCheck == "")
                        nullList.Add(nullCheck);
                }

                foreach (String toRemove in nullList)
                {
                    distinctResults.Remove(toRemove);
                }

                Session["distinctResults"] = distinctResults;

            }
            else
            {
                distinctResults = (List<String>)Session["distinctResults"];
            }

            //Search
            Session["lastFilter"] = filter;           


            if (String.IsNullOrWhiteSpace(term))
                return Json(distinctResults, JsonRequestBehavior.AllowGet);

            

            for (int i = 0; i < distinctResults.Count; i++)
            {
                //Ignore Case (term, true, null)
                if (distinctResults.ElementAt(i).StartsWith(term, true, null))
                    searchResults.Add(distinctResults.ElementAt(i));
            }
            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }
    }
}
