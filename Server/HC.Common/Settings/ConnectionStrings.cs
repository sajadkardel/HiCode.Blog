﻿using HC.Common.Utilities;

namespace HC.Common.Settings;

public class ConnectionStrings
{
    public string SqlServer { get; set; } = default!;

    public static ConnectionStrings Get() => ConfigurationExtensions.GetSection<ConnectionStrings>(nameof(ConnectionStrings));
}