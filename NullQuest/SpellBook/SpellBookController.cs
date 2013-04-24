using BadSnowstorm;
using NullQuest.Data;
using NullQuest.Game;
using NullQuest.Game.Extensions;
using NullQuest.Game.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.SpellBook
{
    public class SpellBookController : Controller
    {
        #region Here There Be Dragons
        private const string DefaultTitle = "Spell Book";
        private const string DefaultInformation = "There are many spells in this world. Some are better left unlearned.";
        private const int PageSize = 3;

        private ISpell _currentSpell;
        private int _currentSpellIndex = -1;
        private int _pageIndex;
        private string _title;
        private string _information;
        #endregion

        private readonly IAsciiArtRepository _asciiArtRepository;
        private readonly IDice _dice;
        
        public SpellBookController()
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

            if (_currentSpell == null)
            {
                var spellBook = GameWorld.Player.SpellBook.ToList();

                foreach (var x in spellBook
                    .Skip(_pageIndex * PageSize)
                    .Take(PageSize)
                    .Select((spell, index) => new { Spell = spell, Index = (_pageIndex * PageSize) + index }))
                {
                    var menuItem = new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = x.Spell.GetLeveledName(),
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () =>
                        {
                            _currentSpell = x.Spell;
                            _currentSpellIndex = x.Index;
                            _title = x.Spell.GetLeveledName();
                            _information = x.Spell.GetDescription();
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

                if (spellBook.Count > ((_pageIndex * PageSize) + PageSize))
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
                if (_currentSpell is INonCombatSpell && _currentSpell.CanCast(GameWorld.Player, new NonCombatContext()))
                {
                    var menuItem = new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = "Cast",
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () =>
                        {
                            var logEntry = ((INonCombatSpell)_currentSpell).Cast(_dice, GameWorld.Player);

                            _title = string.Format("Cast {0}", _currentSpell.GetLeveledName());
                            _information = logEntry != null ? logEntry.Text : null;

                            _currentSpell = null;
                            _currentSpellIndex = -1;
                            if ((_pageIndex * PageSize) > GameWorld.Player.SpellBook.Count())
                            {
                                _pageIndex--;
                            }
                        }
                    };

                    menu.AddMenuItem(menuItem);
                    id++;
                };

                menu.AddMenuItem(
                    new MenuItem
                    {
                        Id = id.ToString()[0],
                        Text = "Move to top",
                        ActionResult = Actions.Reload,
                        OnInputAccepted = () =>
                        {
                            GameWorld.Player.MoveSpellToTopOfSpellBook(_currentSpell, _currentSpellIndex);
                            _currentSpell = null;
                            _currentSpellIndex = -1;
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
                            _currentSpell = null;
                            _currentSpellIndex = -1;
                        }
                    });
            }

            var viewModel = ViewModel.CreateWithMenu<SpellBookViewModel>(menu);

            viewModel.Stats = StatsViewModel.FromPlayer(GameWorld.Player);
            viewModel.Title = _title;
            viewModel.Information = _information;
            viewModel.AsciiArt = _asciiArtRepository.GetSpellBookArt();
            
            return viewModel;
        }
        #endregion
    }
}
