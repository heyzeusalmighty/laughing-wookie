﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Occultation.DAL;
using Occultation.DataModels;
using Occultation.ViewModels;

namespace ExploreSpace.Controllers
{
    public class MapController : ApiController
    {

        [HttpGet]
        //[ActionName("GameMap")]
        public GameMap GetMapTiles()
        {
            //hardcoding this for the time being
            //const string guid = "239d4ec4-1cd8-4beb-af88-6dbd9f361824";
            const string guid = "747ab3e9-a240-478f-b83a-f81ffe69f6a5";
            var model = new BuildMapViewModel(new EFGameRepo());
            return model.GetGameMap(guid);
        }
    }
}
