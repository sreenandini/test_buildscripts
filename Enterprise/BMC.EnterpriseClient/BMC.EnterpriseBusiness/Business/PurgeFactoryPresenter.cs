using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    // A delegate type for hooking up change notifications.
    public delegate void SaveItemsPurgeHandler(List<PurgeCategory> Items );
    public delegate List<PurgeCategory> AddItemsHandler(PurgeCategory Item);
    public delegate List<PurgeTables> LoadTableListHandler();



    public interface IPurgeEditor
    {
         event SaveItemsPurgeHandler SavePurgeItems;
         event AddItemsHandler AddItem;
         event LoadTableListHandler LoadTables;
    }


    public class PurgeViewPresenter
    {
        List<PurgeCategory> purgeList;
        private IPurgeEditor purgeItem;

        private IPurgeFactory iPurge;

        /// <summary>
        /// initialize the presenter.
        /// </summary>
        /// <param name="view"></param>
        public PurgeViewPresenter(IPurgeEditor view)
        {
            this.purgeItem = view;
            purgeList = new List<PurgeCategory>();
            iPurge = PurgeFactoryBiz.CreateInstance();
            this.purgeItem.SavePurgeItems += OnPurgeItemsSaved;
            this.purgeItem.AddItem += OnItemAdded;
            this.purgeItem.LoadTables += OnLoadTablesList;
        }
        /// <summary>
        /// get the list of purge tables.
        /// </summary>
        /// <returns></returns>
        List<PurgeTables> OnLoadTablesList()
        {
            return iPurge.LoadPurgeTablesList();
        }
        /// <summary>
        /// add the item to be purged
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        List<PurgeCategory> OnItemAdded(PurgeCategory Item)
        {
            if (!purgeList.Contains(Item))
                purgeList.Add(Item);
            return purgeList;
        }

        /// <summary>
        /// save the purge category details
        /// </summary>
        /// <param name="Items"></param>
        void OnPurgeItemsSaved(List<PurgeCategory> Items)
        {
            iPurge.SavePurgeCategoryDetails(Items);
        }

    }
}