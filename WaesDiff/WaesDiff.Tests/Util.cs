namespace WaesDiff.Tests
{
    using System;

    using WaesDiff.Domain.Entities;

    public static class Util
    {
        private const string SameSizeA = "VGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIDcwIGxhenkgZG9ncy4gUGx1cyBzb21ldGhpbmcgMTIzNDU2IG1vcmUgMTIz";

        private const string DifferentSize = "VGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIDE0IGxhenkgZG9ncy4gUGx1cyBzb21ldGhpbmcgbW9yZQ==";
        
        private const string SameSizeB = "dGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIDcwIGxhenkgZG9ncy4gUGx1cyBzb21ldGhpbmcgNjU0MzIxIG1vcmUgMzIx";

        internal static DataEntity DataEntitySameSizeA => new DataEntity
                                                      {
                                                          Data = SameSizeA,
                                                          DataBase64 = Convert.FromBase64String(SameSizeA)
                                                      };

        internal static DataEntity DataEntityDifferentSize => new DataEntity
                                                      {
                                                          Data = DifferentSize,
                                                          DataBase64 = Convert.FromBase64String(DifferentSize)
                                                      };

        internal static DataEntity DataEntitySameSizeB => new DataEntity
                                                      {
                                                          Data = SameSizeB,
                                                          DataBase64 = Convert.FromBase64String(SameSizeB)
                                                      };
    }
}
