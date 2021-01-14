using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEntity
{
    public class Rocket
    {
        public string rocket_name { get; set; }
    }

    /// <summary>
    /// Represents Links from the SpaceX API
    /// </summary>
    public class Links
    {
        public string Mission_patch { get; set; }
        public string Mission_patch_small { get; set; }
        public string Reddit_campaign { get; set; }
        public string Reddit_launch { get; set; }
        public string Reddit_recovery { get; set; }
        public string Reddit_media { get; set; }
        public string Presskit { get; set; }
        public string Article_link { get; set; }
        public string Wikipedia { get; set; }
        public string Video_link { get; set; }
        public string Youtube_id { get; set; }
        public List<object> Flickr_images { get; set; }
    }

    class Launch
    {
        public Rocket Rocket { get; set; }
        public int Flight_number { get; set; }
        public string Mission_name { get; set; }
        public string Launch_year { get; set; }
        public DateTime Launch_date_utc { get; set; }
        public Links Links { get; set; }
        public string Details { get; set; }

        public Entity ToEntity(ITracingService tracingService)
        {
            Entity entity = new Entity("irin3_spacexrocketlaunch");

            // Transform int unique value to Guid
            var id = Flight_number;
            var uniqueIdentifier = CDPHelper.IntToGuid(id);
            tracingService.Trace("Flight Number: {0} transformed into Guid: {1}", Flight_number, uniqueIdentifier);

            // Map data to entity
            entity["irin3_spacexrocketlaunchid"] = uniqueIdentifier;
            entity["irin3_name"] = Mission_name;
            entity["irin3_flightnumber"] = Flight_number;
            entity["irin3_rocket"] = Rocket.rocket_name;
            entity["irin3_launchyear"] = Launch_year;
            entity["irin3_launchdate"] = Launch_date_utc;
            entity["irin3_missionpatch"] = Links.Mission_patch;
            entity["irin3_presskit"] = Links.Presskit;
            entity["irin3_videolink"] = Links.Video_link;
            entity["irin3_wikipedia"] = Links.Wikipedia;
            entity["irin3_details"] = Details;

            return entity;
        }
    }
}
