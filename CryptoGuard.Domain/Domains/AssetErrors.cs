using CryptoGuard.Domain.Abstractions;

namespace CryptoGuard.Domain.Domains;

public static class AssetErrors
 {
     public static readonly Error NegativePrice = new(
         "Asset.NegativePrice", 
         "Cena aktiva nesmí být záporná.");

     public static readonly Error PriceJumpTooBig = new(
         "Asset.PriceJumpTooBig", 
         "Změna ceny je příliš prudká (o více než 50 %).");

     public static readonly Error SamePrice = new(
         "Asset.SamePrice", 
         "Nová cena je totožná s aktuální cenou.");

     public static readonly Error InvalidSymbol = new(
         "Asset.InvalidSymbol", 
         "Symbol aktiva musí být vyplněn.");
     
        public static readonly Error AssetNotFound = new(
         "Asset.NotFound", 
         "Asset nebyl nalezen.");
 }