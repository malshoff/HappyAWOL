using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Config.Net;

namespace HappyRebellion.Config {
    public interface IMySettings {
        int RebellionRelationsChange { get; }
        int ForfeitSettlementsRelationsChange { get; }
        bool DeclareWarOnRebellion { get; }

    }
}
