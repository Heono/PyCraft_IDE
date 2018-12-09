using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using Dragablz;
using Dragablz.Dockablz;


namespace PyCraft_IDE_WPF
{
    public class MainModel
    {
        private readonly ObservableCollection<HeaderedItemViewModel> _items;
        private readonly ObservableCollection<HeaderedItemViewModel> _toolItems = new ObservableCollection<HeaderedItemViewModel>();

        public MainModel() { _items = new ObservableCollection<HeaderedItemViewModel>(); }

        public MainModel(params HeaderedItemViewModel[] items)  { _items = new ObservableCollection<HeaderedItemViewModel>(items); }

        public ObservableCollection<HeaderedItemViewModel> Items { get { return _items; } }

        public static Guid TabPartition { get { return new Guid("2AE89D18-F236-4D20-9605-6C03319038E6"); } }

        public ObservableCollection<HeaderedItemViewModel> ToolItems { get { return _toolItems; } }

        public ItemActionCallback ClosingTabItemHandler { get { return ClosingTabItemHandlerImpl; } }

        /// <summary>
        /// Callback to handle tab closing.
        /// </summary>        
        private static void ClosingTabItemHandlerImpl(ItemActionCallbackArgs<TabablzControl> args)
        {
            //in here you can dispose stuff or cancel the close

            //here's your view model:
            var viewModel = args.DragablzItem.DataContext as HeaderedItemViewModel;
            Debug.Assert(viewModel != null);


            //here's how you can cancel stuff:
            // args.Cancel();
        }

        public ClosingFloatingItemCallback ClosingFloatingItemHandler
        {
            get { return ClosingFloatingItemHandlerImpl; }
        }

        /// <summary>
        /// Callback to handle floating toolbar/MDI window closing.
        /// </summary>        
        private static void ClosingFloatingItemHandlerImpl(ItemActionCallbackArgs<Layout> args)
        {
            //in here you can dispose stuff or cancel the close

            //here's your view model: 
            var disposable = args.DragablzItem.DataContext as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            //here's how you can cancel stuff:
            //args.Cancel(); 
        }
    }
}
