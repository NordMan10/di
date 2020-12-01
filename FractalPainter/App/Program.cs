﻿using System;
using System.Windows.Forms;
using Ninject;
using FractalPainting.Infrastructure.UiActions;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
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

                //container.Bind<IUiAction>().To<SaveImageAction>();
                //container.Bind<IUiAction>().To<DragonFractalAction>();
                //container.Bind<IUiAction>().To<KochFractalAction>();
                //container.Bind<IUiAction>().To<ImageSettingsAction>();
                //container.Bind<IUiAction>().To<PaletteSettingsAction>();

                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();

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