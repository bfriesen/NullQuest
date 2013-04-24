using System.Linq;
using BadSnowstorm;
using NullQuest.Game.Extensions;
using NullQuest.Game.Items;
using NullQuest.Game.Combat;
using NullQuest.Data;

namespace NullQuest.Game.Inventory
{
    public class InventoryController : Controller
    {
        #region Here There Be Dragons
        private const string DefaultTitle = "Inventory";
        private const string DefaultInformation = "Your bag can hold many things, but be mindful: a heavy bag may slow you down.";
        private const int PageSize = 5;

        private IItem _currentItem;
        private int _currentItemIndex = -1;
        private int _pageIndex;
        private string _title;
        private string _information;
        #endregion

        private readonly IAsciiArtRepository _asciiArtRepository;
        private readonly IDice _dice;
        
        public InventoryController()
        {
            _asciiArtRepository = new HardCodedAsciiArtRepository();
            _dice = new Dice();

            Init();
        }

        #region Here There Be Dragons
        private void Init()
        {
            _title = DefaultTitle;
            _information = DefaultInformation;
        }
        
        public override ViewModel Index()
        {
            var menu = new Menu();
            var id = 1;

            if (_currentItem == null)
            {
                var inventory = GameWorld.Player.Inventory.Where(item => item.Quantity > 0).ToList();

                foreach (var x in inventory
                    .Skip(_pageIndex * PageSize)
                    .Take(PageSize)
                    .Select((item, index) => new { Item = item, Index = (_pageIndex * PageSize) + index }))
                {
                    var menuItem = new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = x.Item.GetLeveledName() + (x.Item.Quantity > 1 ? string.Format(" ({0})", x.Item.Quantity) : ""),
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () =>
                        {
                            _currentItem = x.Item;
                            _currentItemIndex = x.Index;
                            _title = x.Item.GetLeveledName();
                            _information = x.Item.GetDescription();
                        }
                    };

                    menu.AddMenuItem(menuItem);
                    id++;
                }

                if (_pageIndex > 0)
                {
                    var menuItem = new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = "Previous",
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () => _pageIndex--
                    };

                    menu.AddMenuItem(menuItem);
                    id++;
                }

                if (inventory.Count > ((_pageIndex * PageSize) + PageSize))
                {
                    var menuItem = new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = "Next",
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () => _pageIndex++
                    };

                    menu.AddMenuItem(menuItem);
                    id++;
                }

                menu.AddMenuItem(
                    new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = "Back",
                        ActionResult = Actions.GoBack
                    });
            }
            else
            {
                if (_currentItem is INonCombatItem && _currentItem.CanUse(GameWorld.Player, new NonCombatContext()))
                {
                    var menuItem = new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = _currentItem is Weapon ? "Equip" : "Use",
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () =>
                        {
                            var logEntry = ((INonCombatItem)_currentItem).Use(_dice, GameWorld.Player);
                         
                            _title = (_currentItem is Weapon ? "Equipped " : "Used 1 ") + _currentItem.GetLeveledName();
                            _information = logEntry != null ? logEntry.Text : null;

                            _currentItem = null;
                            _currentItemIndex = -1;
                            if ((_pageIndex * PageSize) > GameWorld.Player.Inventory.Count(item => item.Quantity > 0))
                            {
                                _pageIndex--;
                            }
                        }
                    };

                    menu.AddMenuItem(menuItem);
                    id++;
                };

                if (!(_currentItem is Weapon) || !_currentItem.Equals(Weapon.BareHands))
                {
                    menu.AddMenuItem(
                        new MenuItem
                        {
                            Id = id.ToString()[0],
                            Text = "Discard",
                            ActionResult = Actions.Reload,
                            OnInputAccepted = () =>
                            {
                                _title = DefaultTitle;
                                _information = "Discarded 1 " + _currentItem.Name;

                                GameWorld.Player.RemoveItemFromInventory(_currentItem);
                                _currentItem = null;
                                _currentItemIndex = -1;
                                if ((_pageIndex * PageSize) > GameWorld.Player.Inventory.Count(item => item.Quantity > 0))
                                {
                                    _pageIndex--;
                                }
                            }
                        });
                    id++;
                }

                menu.AddMenuItem(
                    new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = "Move to top",
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () =>
                        {
                            GameWorld.Player.MoveItemToTopOfInventory(_currentItem, _currentItemIndex);
                            _currentItem = null;
                            _currentItemIndex = -1;
                        }
                    });
                id++;

                menu.AddMenuItem(
                    new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = "Back",
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () =>
                        {
                            _title = DefaultTitle;
                            _information = DefaultInformation;
                            _currentItem = null;
                            _currentItemIndex = -1;
                        }
                    });
            }

            var viewModel = ViewModel.CreateWithMenu<InventoryViewModel>(menu);

            viewModel.Stats = StatsViewModel.FromPlayer(GameWorld.Player);
            viewModel.Title = _title;
            viewModel.Information = _information;
            viewModel.AsciiArt = _asciiArtRepository.GetInventoryArt();
            
            return viewModel;
        }
        #endregion
    }
}
