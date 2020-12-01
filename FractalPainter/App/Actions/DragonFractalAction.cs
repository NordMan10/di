using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        //private IImageHolder imageHolder;

        //private readonly Func<DragonSettings, DragonPainter> dragonPainterFactory;
        private readonly Func<DragonSettings, DragonPainter> dragonPainterFactory;
        private readonly Func<DragonSettingsGenerator> dragonSettingsGeneratorFactory;


        public DragonFractalAction(Func<DragonSettingsGenerator> dragonSettingsGeneratorFactory,
            Func<DragonSettings, DragonPainter> dragonPainterFactory/*IDragonPainterFactory dragonPainterFactory*/)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsGeneratorFactory = dragonSettingsGeneratorFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = dragonSettingsGeneratorFactory().Generate();
            var dragonPainter = dragonPainterFactory(dragonSettings);
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            //var container = new StandardKernel();
            //container.Bind<IImageHolder>().ToConstant(imageHolder);
            //container.Bind<DragonSettings>().ToConstant(dragonSettings);
            //container.Get<DragonPainter>().Paint();
            dragonPainter.Paint();

        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }

    public interface IDragonPainterFactory
    {
        public DragonPainter GetDragonPainter(DragonSettings settings);
    }
}