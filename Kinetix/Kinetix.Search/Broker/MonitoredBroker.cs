using Kinetix.Search.ComponentModel;
using Kinetix.Search.Contract;
using Kinetix.Search.Model;
using System;
using System.Collections.Generic;

namespace Kinetix.Search.Broker
{
    /// <summary>
    /// Décorateur de monitoring des brokers de recherche.
    /// </summary>
    /// <typeparam name="TDocument">Type du document.</typeparam>
    public class MonitoredBroker<TDocument> : ISearchBroker<TDocument>
    {
        private readonly ISearchBroker<TDocument> _broker;

        /// <summary>
        /// Créé une nouvelle instance de MonitoredBroker.
        /// </summary>
        /// <param name="broker">Broker à décorer.</param>
        public MonitoredBroker(ISearchBroker<TDocument> broker)
        {
            if (broker == null)
            {
                throw new ArgumentNullException(nameof(broker));
            }

            _broker = broker;
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.CreateDocumentType" />
        public void CreateDocumentType()
        {
            _broker.CreateDocumentType();
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.Get" />
        public TDocument Get(string id)
        {
            return _broker.Get(id);
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.Put" />
        public void Put(TDocument document)
        {
            _broker.Put(document);
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.PutAll" />
        public void PutAll(IEnumerable<TDocument> documentList)
        {
            _broker.PutAll(documentList);
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.Remove" />
        public void Remove(string id)
        {
            _broker.Remove(id);
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.Flush" />
        public void Flush()
        {
            _broker.Flush();
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.Query" />
        public IEnumerable<TDocument> Query(string text, string security = null)
        {
            return _broker.Query(text, security);
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.AdvancedQuery" />
        public QueryOutput<TDocument> AdvancedQuery(AdvancedQueryInput input)
        {
            return _broker.AdvancedQuery(input);
        }

        /// <inheritdoc cref="ISearchBroker{TDocument}.AdvancedCount" />
        public long AdvancedCount(AdvancedQueryInput input)
        {
            return _broker.AdvancedCount(input);
        }
    }
}
