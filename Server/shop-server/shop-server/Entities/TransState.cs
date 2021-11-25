using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Entities
{
    //便利商店取貨
    enum TransStoreState
    {
        InStorage,  //還在庫存尚未出貨
        InSellStore,  //賣家已將商品送到便利商店
        SellStoreWaiting , //便利商店收到貨，等待運送
        Transport,  //運送中
        InBuyStore,  //運送完成，已經到買家指定的便利商店
        BuyerGet, //買家取貨
    }
    
    //直接運送到指定地址
    enum TransToAddress
    {
        InStorage , //商品還在庫存，賣家尚未出貨
        Transport , //運送中
        InBuyAddress , //送到買家的地址，但尚未取貨
        BuyerGet // 買家取貨
    }
}
