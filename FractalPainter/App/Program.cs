﻿using System;
using System.Windows.Forms;
using Ninject;
using FractalPainting.Infrastructure.UiActions;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.App.Fractals;
using Ninject.Extensions.Conventions;


namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();

                container.Rebind<IUiAction>().To<SaveImageAction>();
                //container.Bind<IUiAction>().To<DragonFractalAction>();
                //container.Bind<IUiAction>().To<KochFractalAction>();
                //container.Bind<IUiAction>().To<ImageSettingsAction>();
                //container.Bind<IUiAction>().To<PaletteSettingsAction>();

                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();

                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>().WhenInjectedInto<SettingsManager>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>().WhenInjectedInto<SettingsManager>();
                container.Bind<IImageDirectoryProvider>().To<AppSettings>();

                container.Bind<AppSettings>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
                container.Bind<ImageSettings>().ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings);

                container.Bind(c => c.FromThisAssembly().SelectAllClasses().InheritedFrom<IUiAction>().BindAllInterfaces());


                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}