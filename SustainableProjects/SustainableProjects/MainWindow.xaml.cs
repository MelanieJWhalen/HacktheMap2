using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SustainableProjects
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FeatureLayerUrl = "https://maps.raleighnc.gov/arcgis/rest/services/Sustainable/MapServer/0";

        FeatureCollectionTable _featCollectionTable;
        FeatureCollection _featCollection;
        FeatureCollectionLayer _featCollectionLayer;
        ServiceFeatureTable _featureTable;
        FeatureLayer _featureLayer;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // Create a new map with the topographic basemap and add it to the map view
            Map myMap = new Map(Basemap.CreateTopographic());
            MyMapView.Map = myMap;

            // Create and set initial map location
            MapPoint initialLocation = new MapPoint(-8753528.496, 4272076.724, SpatialReferences.WebMercator);
            myMap.InitialViewpoint = new Viewpoint(initialLocation, 100000);

            // Create a service feature table
            _featureTable = new ServiceFeatureTable(new Uri(FeatureLayerUrl));
            _featureLayer = new FeatureLayer(_featureTable);
            myMap.OperationalLayers.Add(_featureLayer);

            GetFeaturesFromQuery();
            PopulateCategoryCombo();
        }

        private async void GetFeaturesFromQuery()
        {

            // Create a query to get all features in the table
            QueryParameters queryParams = new QueryParameters();
            queryParams.WhereClause = "1=1";

            // Query the table to get all features
            FeatureQueryResult featureResult = await _featureTable.QueryFeaturesAsync(queryParams);

            // Create a new feature collection table from the result features
            _featCollectionTable = new FeatureCollectionTable(featureResult);

            // Create a feature collection and add the table
            _featCollection = new FeatureCollection();
            _featCollection.Tables.Add(_featCollectionTable);

            //// Create a layer to display the feature collection, add it to the map's operational layers
            //_featCollectionLayer = new FeatureCollectionLayer(_featCollection);
            //MyMapView.Map.OperationalLayers.Add(_featCollectionLayer);
        }

        private void PopulateCategoryCombo()
        {
            IList<string> categories;
            Field catField = _featCollectionTable.GetField("Category");
            //foreach (var feat in _featCollectionTable)
              //  categories.Add(feat.GetAttributeValue(catField).ToString());      
        }

        private void CategoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = e.AddedItems[0].ToString();

            // Get index that is used to get the selected url
            //var selectedIndex = _titles.ToList().IndexOf(selectedMap);

            // Create a new Map instance with url of the webmap that selected
            //MyMapView.Map = new Map(new Uri(_itemURLs[selectedIndex]));


        }
    }
}
