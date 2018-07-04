﻿using Kinetix.Search.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kinetix.Search.Broker
{

    /// <summary>
    /// Manager pour les brokers de recherche.
    /// </summary>
    public sealed class SearchBrokerManager
    {

        /// <summary>
        /// Constante.
        /// </summary>
        internal const string SearchCube = "SEARCHDB";
        private static readonly SearchBrokerManager _instance = new SearchBrokerManager();
        private static string _defaultDataSourceName;

        private readonly Dictionary<string, ISearchBroker> _brokerMap = new Dictionary<string, ISearchBroker>();
        private readonly Dictionary<string, Type> _storeMap = new Dictionary<string, Type>();

        /// <summary>
        /// Constructeur.
        /// </summary>
        private SearchBrokerManager()
        {
        }

        /// <summary>
        /// Retourne une instance du manager.
        /// </summary>
        public static SearchBrokerManager Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Enregistre la source de données.
        /// </summary>
        /// <param name="dataSource">Source de données.</param>
        public static void RegisterDefaultDataSource(string dataSource)
        {
            if (string.IsNullOrEmpty(dataSource))
            {
                throw new ArgumentNullException("dataSource");
            }

            _defaultDataSourceName = dataSource;
        }

        /// <summary>
        /// Retourne l'instance du broker associé au type.
        /// </summary>
        /// <typeparam name="T">Type du broker.</typeparam>
        /// <returns>Le broker.</returns>
        public static ISearchBroker<T> GetBroker<T>()
            where T : class, new()
        {
            return Instance.ReturnBroker<T>(null);
        }

        /// <summary>
        /// Enregistre un nouveau store.
        /// </summary>
        /// <param name="dataSourceName">Nom de la source de données.</param>
        /// <param name="storeType">Type du store à enregistrer.</param>
        public void RegisterStore(string dataSourceName, Type storeType)
        {
            if (dataSourceName == null)
            {
                throw new ArgumentNullException("dataSourceName");
            }

            if (storeType == null)
            {
                throw new ArgumentNullException("storeType");
            }

            ILog log = LogManager.GetLogger("Application");
            if (log.IsDebugEnabled)
            {
                log.Debug("Enregistrement du search store " + dataSourceName + " du type " + storeType.FullName);
            }

            _storeMap[dataSourceName] = storeType;
        }

        /// <summary>
        /// Retourne le type de store à utiliser pour une source de données.
        /// </summary>
        /// <param name="dataSourceName">Nom de la source de données.</param>
        /// <returns>Type de store à utiliser.</returns>
        internal Type GetStoreType(string dataSourceName)
        {
            return _storeMap[dataSourceName];
        }

        /// <summary>
        /// Retourne l'instance du broker associé au type.
        /// </summary>
        /// <typeparam name="T">Type du broker.</typeparam>
        /// <param name="dataSourceName">Source de données : source par défaut si nulle.</param>
        /// <returns>Le broker.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "La variable basicBroker n'est au final castée qu'une fois.")]
        private ISearchBroker<T> ReturnBroker<T>(string dataSourceName)
            where T : class, new()
        {
            string dsName = dataSourceName;
            if (string.IsNullOrEmpty(dsName))
            {
                if (string.IsNullOrEmpty(_defaultDataSourceName))
                {
                    throw new NotSupportedException("dataSource not registered, call first method RegisterDefaultDataSource");
                }

                dsName = _defaultDataSourceName;
            }

            string key = typeof(T).AssemblyQualifiedName + "/" + dsName;
            ISearchBroker broker;
            if (_brokerMap.TryGetValue(key, out broker))
            {
                return (ISearchBroker<T>)broker;
            }

            lock (_brokerMap)
            {
                if (_brokerMap.TryGetValue(key, out broker))
                {
                    return (ISearchBroker<T>)broker;
                }

                broker = new MonitoredBroker<T>(new StandardSearchBroker<T>(dsName));

                _brokerMap[key] = broker;
                return (ISearchBroker<T>)broker;
            }
        }
    }
}
