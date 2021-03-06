﻿using FirstApp.Core.Entities;
using System.Collections.Generic;

namespace FirstApp.Core.Interfaces
{
    public interface IMapMarkerService
    {
        void InsertMarker(MapMarkerEntity marker);
        List<MapMarkerEntity> GetMarkerList(int taskId);
        void DeleteMarkers(int taskId);
    }
}
