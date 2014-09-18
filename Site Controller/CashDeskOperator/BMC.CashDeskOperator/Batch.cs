using System.Collections.Generic;
using BMC.Business.CashDeskOperator;
using BMC.Transport;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class Batch : IBatch
    {
        readonly BatchhistoryBreakdown _batchHistoryBreakdown;

        public Batch(int collectionID, int installationNo, int batchID, string ExchangeConst, int weekId)
        {
            _batchHistoryBreakdown = new BatchhistoryBreakdown(collectionID, installationNo, batchID, ExchangeConst, weekId);
        }

        public void GetBatchBreakdownhistory()
        {
            _batchHistoryBreakdown.GetBatchBreakdownhistory();
        }

        public CollectionView GetCollectionData(int CollectionNo)
        {
            return _batchHistoryBreakdown.GetCollectionData(CollectionNo);

        }
        
        public List<Transport.AllEvents> GetAllEvents(int collectionNo, int installationNo,int Top)
        {
            return _batchHistoryBreakdown.GetAllEvents(collectionNo, installationNo, Top);
        }

        public List<Transport.BatchHistoryListView> GetBatchDetails(out BatchDetails details)
        {
            return _batchHistoryBreakdown.GetBatchDetails(out details);
        }


        public List<Transport.CollectionListView> GetCollectionDetailsforListView(CollectionView _collectionRecords)
        {
            return _batchHistoryBreakdown.GetCollectionDetailsforListView(_collectionRecords);
        }

        public List<Transport.PartCollectionUser> GetCollectionUser(CollectionView _collectionRecords)
        {
            return _batchHistoryBreakdown.GetCollectionUser(_collectionRecords);
        }
        
        public List<Transport.TreasuryUser> GetTreasuryTable(CollectionView _collectionview)
        {
            return _batchHistoryBreakdown.GetTreasuryTable(_collectionview);
        }

        public List<Transport.rsp_AssetVarianceHistoryResult> GetAssetVarianceHistory(int installationNo, int recordCount)
        {
            return _batchHistoryBreakdown.GetAssetVarianceHistory(installationNo, recordCount);
        }
    }
}
