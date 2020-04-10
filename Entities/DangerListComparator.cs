using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Entities
{
    public class DangerListComparator:IListComparator<Danger>
    {
        private List<Danger> oldList, newList;
        public DangerListComparator(List<Danger> oldList, List<Danger> newList)
        {
            this.oldList = oldList;
            this.newList = newList;
        }
        public List<Danger> getNewComponents()
        {
            return newList.Except(oldList).ToList();
        }

        public List<Danger> getChangedComponents()
        {
            var changedComponents = new List<Danger>();
            foreach (var newDanger in newList)
            {
                try
                {
                    var oldDanger = oldList[newDanger.Id-1];
                    if (!newDanger.EqualsByField(oldDanger))
                    {
                        changedComponents.Add(newDanger);
                    }
                }
                catch (Exception)
                {
                   
                }
            }

            return changedComponents;
        }

        public List<ChangedObject> getChangeList(Danger oldDanger, Danger newDanger)
        {
            var changeList = new List<ChangedObject>();
            if (newDanger.Description != oldDanger.Description)
            {
                changeList.Add(new ChangedObject( "Описание",oldDanger.Description, newDanger.Description));
            }

            if (newDanger.Source != oldDanger.Source)
            {
                changeList.Add(new ChangedObject("Источник", oldDanger.Source, newDanger.Source));
            }

            if (newDanger.Objective != oldDanger.Objective)
            {
                changeList.Add(new ChangedObject( "Объект воздейтсвия", oldDanger.Objective, newDanger.Objective));
            }

            if (newDanger.IsPrivacyViolation != oldDanger.IsPrivacyViolation)
            {
                changeList.Add(new ChangedObject("Нарушение конфиденциальности",oldDanger.IsPrivacyViolation, newDanger.IsPrivacyViolation));
            }

            if (newDanger.IsAccessViolation != oldDanger.IsAccessViolation)
            {
                changeList.Add(new ChangedObject("Нарушение доступности",oldDanger.IsAccessViolation, newDanger.IsAccessViolation));

            }

            if (newDanger.IsIntegrityViolation != oldDanger.IsIntegrityViolation)
            {
                changeList.Add(new ChangedObject("Нарушение целостности",oldDanger.IsIntegrityViolation,newDanger.IsIntegrityViolation));
            }

            return changeList;
        }

        public int GetUpdatedCount()
        {
            return getChangedComponents().Count + getNewComponents().Count;
        }
    }
}