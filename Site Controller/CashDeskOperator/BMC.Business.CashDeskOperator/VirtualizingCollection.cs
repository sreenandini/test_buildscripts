using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BMC.Business.CashDeskOperator
{
    public static class ListViewHelper
    {
        /// <summary>
        /// Expands all children of a ListView
        /// </summary>
        /// <param name="ListView">The ListView whose children will be Selected</param>
        public static void ExpandAll(this ListView ListView)
        {
            ExpandSubContainers(ListView);
        }

        /// <summary>
        /// Expands all children of a ListView or ListViewItem
        /// </summary>
        /// <param name="parentContainer">The ListView or ListViewItem containing the children to expand</param>
        private static void ExpandSubContainers(ItemsControl parentContainer)
        {
            foreach (Object item in parentContainer.Items)
            {
                ListViewItem currentContainer = parentContainer.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                if (currentContainer != null)
                {
                    //expand the item
                    currentContainer.IsSelected = true;

                }
            }
        }

        /// <summary>
        /// Searches a ListView for the provided object and selects it if found
        /// </summary>
        /// <param name="ListView">The ListView containing the item</param>
        /// <param name="item">The item to search and select</param>
        public static void SelectItem(this ListView ListView, object item)
        {
            ExpandAndSelectItem(ListView, item);
        }

        /// <summary>
        /// Finds the provided object in an ItemsControl's children and selects it
        /// </summary>
        /// <param name="parentContainer">The parent container whose children will be searched for the selected item</param>
        /// <param name="itemToSelect">The item to select</param>
        /// <returns>True if the item is found and selected, false otherwise</returns>
        private static bool ExpandAndSelectItem(ItemsControl parentContainer, object itemToSelect)
        {
            //check all items at the current level
            foreach (Object item in parentContainer.Items)
            {
                ListViewItem currentContainer = parentContainer.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;

                //if the data item matches the item we want to select, set the corresponding
                //ListViewItem IsSelected to true
                if (item == itemToSelect && currentContainer != null)
                {
                    currentContainer.IsSelected = true;
                    currentContainer.BringIntoView();
                    currentContainer.Focus();

                    //the item was found
                    return true;
                }
            }

            //if we get to this point, the selected item was not found at the current level, so we must check the children
            foreach (Object item in parentContainer.Items)
            {
                ListViewItem currentContainer = parentContainer.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;

                //if children exist
                if (currentContainer != null)
                {
                    //keep track of if the ListViewItem was Selected or not
                    bool wasSelected = currentContainer.IsSelected;

                    //expand the current ListViewItem so we can check its child ListViewItems
                    currentContainer.IsSelected = true;
                    return true;
                }
            }

            //no item was found
            return false;
        }
    }
}
