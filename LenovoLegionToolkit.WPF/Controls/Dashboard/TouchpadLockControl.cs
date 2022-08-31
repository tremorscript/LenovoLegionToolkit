﻿using System.Threading.Tasks;
using LenovoLegionToolkit.Lib;
using LenovoLegionToolkit.Lib.Features;
using LenovoLegionToolkit.Lib.Listeners;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace LenovoLegionToolkit.WPF.Controls.Dashboard
{
    public class TouchpadLockControl : AbstractToggleFeatureCardControl<TouchpadLockState>
    {
        private readonly DriverKeyListener _listener = IoCContainer.Resolve<DriverKeyListener>();

        protected override TouchpadLockState OnState => TouchpadLockState.On;

        protected override TouchpadLockState OffState => TouchpadLockState.Off;

        public TouchpadLockControl()
        {
            Icon = SymbolRegular.Tablet24;
            Title = "Touchpad Lock";
            Subtitle = "Disable touchpad.";

            _listener.Changed += Listener_Changed;
        }

        protected override Task OnStateChange(ToggleSwitch toggle, IFeature<TouchpadLockState> feature)
        {
            _listener.IgnoreNext();

            return base.OnStateChange(toggle, feature);
        }

        private void Listener_Changed(object? sender, DriverKey e) => Dispatcher.Invoke(async () =>
        {
            if (!IsLoaded || !IsVisible)
                return;

            if (e == DriverKey.Fn_F10)
                await RefreshAsync();
        });
    }
}