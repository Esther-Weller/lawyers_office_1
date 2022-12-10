﻿using System.Collections.Generic;
using EntitiesDTO;

namespace Dal.converters
{
    public class ActionsConverter
    {
        public static ActionsDto toDto(models.Action obj)
        {
            return new ActionsDto { 
                Id= obj.Id,
                ActionPatternId= obj.ActionPatternId,
                Comments= obj.Comments,
                DeadLine= obj.DeadLine,
                ActionState= obj.ActionState,
                ActionFileId = obj.ActionFileId,
                ActionPriority = obj.ActionPriority,
                CreatedDate = obj.CreatedDate, 
                ActionPatternName = obj.ActionPattern?.ActionPatternName,
                Discription = obj.ActionPattern?.Discription,
                LinkName = obj.ActionPattern?.Link?.LinkName,
                SiteAddress = obj.ActionPattern?.Link?.SiteAddress,
            };
        }
        public static models.Action toDal(ActionsDto obj)
        {
            return new models.Action {
                Id = obj.Id,
                ActionPatternId = obj.ActionPatternId,
                Comments = obj.Comments,
                DeadLine = obj.DeadLine,
                ActionState = obj.ActionState,
                ActionFileId = obj.ActionFileId,
                ActionPriority = obj.ActionPriority,
                CreatedDate = obj.CreatedDate,
                //ActionPattern = new() { ActionPatternName = obj.ActionPatternName },
            };
        }
        public static List<ActionsDto> toDtoList(List<models.Action> objList)
        {
            List<ActionsDto> l = new List<ActionsDto>();
            foreach (models.Action item in objList)
            {
                l.Add(toDto(item));
            }
            return l;
        }
        public static List<models.Action> toDalList(List<ActionsDto> objList)
        {
            List<models.Action> l = new List<models.Action>();
            foreach (ActionsDto item in objList)
            {
                l.Add(toDal(item));
            }
            return l;
        }
    }
}
