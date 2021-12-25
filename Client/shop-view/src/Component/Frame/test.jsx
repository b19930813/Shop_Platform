import * as React from 'react';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { makeStyles } from '@material-ui/core';
import { fabClasses } from '@mui/material';
import axios from 'axios';
import { config } from '../../api/config'

//畫面css

const useStyles = makeStyles(theme => ({
    basic: {
        //paddingTop : 300,
        paddingTop: "20px"
    },
}));

export default function Test() {
    const classes = useStyles();
    const userId = parseInt(localStorage.getItem("userId"));
    const [user, setUser] = React.useState({
        Account: "Test1111",
        Password: "Test1111",
        Name: "Frank",
        Address: "KH",
        Phone: "09123456",
        LineID: "12345678"
    })



    const [commodity, setCommodity] = React.useState({
        Name: "Apple",
        Classification: "Fruit",
        Describe: "可食用水果",
        Price: 3000,

    })

    const [commodityTest, setCommodityTest] = React.useState({
        Name: "Apple",
        Classification: "Fruit",
        Describe: "可食用水果",
        Price: 3000
    })

    const [store, setStore] = React.useState({
        Name: "Frank Store",
        Classification: "Food",
        Describe: "賣食物的商店",
        Subsscription: 100,
        GoodEvaluation: 300,
        BadEvaluation: 0,
        NormalEvaluation: 200,
        Commodities: [commodityTest]
    })

    const [order, setOrder] = React.useState({
        Status: "待出貨",
        UserId: userId,
        StoreId: 9487
    });

    //function
    const handleGetUserclick = () => {
        axios.get('/api/User', config)
            .then(response => {
                console.log("取得/user清單")
                console.log(response)
            })
    }

    const handleCreateUserClick = () => {
        axios.post('api/User', user, config)
            .then(response => {
                console.log(response)
            })
    }

    const handleCreateCommClick = () => {
        axios.post('api/Commodity', commodity, config)
            .then(response => {
                console.log(response)
            })
    }

    const HandleTestAPIClick = () => {
        // axios.get('api/Picture/GetImage/1' , config)
        // .then(response => {
        //     console.log(response.data.message)
        // })
        console.log(localStorage.getItem('test'))
    }

    const handleUpdateUser = () => {
        //先取得資料
        axios.get('/api/User/1', config)
            .then(response => {
                console.log(response)
                var getData = response.data
                getData.address = 'Test UpDate Address'
                //再次發送api
                getData.userId = 2
                axios.put('api/User', getData, config)
                    .then(updateResponse => {
                        console.log(updateResponse)
                    })
            })
    }

    //驗證商店跟商品的關聯性
    const handleCreateStore = () => {
        //先加商品
        // axios.post('api/Commodity', commodity, config)
        //     .then(response => {
        //         console.log(response)
        //     })
        //再加入商店

        // axios.get('api/Commodity', config)
        //     .then(response => {
        //         console.log(response.data)
        // setCommodityTest(response.data)
        // axios.post('api/Store', store, config)
        // .then(response => {
        //     console.log(response)
        // })
        // })

        // console.log(store)

        //查詢store紀錄
        // axios.get('api/Store', config)
        //     .then(response => {
        //         console.log(response)
        //     })
        axios.get('api/User/LoginState', config)
            .then(response => {
                console.log(response.data)

            })
    }

    //創建訂單
    const handleCreateOrderClick = () => {
        console.log(order);
        axios.post('api/Order', order, config)
            .then(response => {
                console.log(response)
            })
        console.log("創建訂單");
    }

    //查詢所有訂單
    const handleQueryAllOrderClick = () => {
        axios.get('api/Order', config)
            .then(response => {
                console.log(response)
            })
        console.log("查詢所有訂單");
    }

    //查詢目前使用者的訂單
    const handleQueryCurrUserOrderClick = () => {
        //axios.get(`'api/Order/GetOrder/${props.userData.id}'`, config)
        console.log(userId);
        console.log('api/Order/GetOrderByUserId/' + userId);
        axios.get('api/Order/GetOrderByUserId/' + userId, config)
            .then(response => {
                console.log(response)
            })
        console.log("查詢目前使用者的訂單");
    }


    return (
        <div className={classes.basic}>
            <Stack spacing={2} direction="row" classes={classes.basic}>
                <Button variant="text" onClick={handleGetUserclick}>查使用者List</Button>
                <Button variant="contained" onClick={handleCreateUserClick}>建立使用者</Button>
                <Button variant="contained" onClick={handleUpdateUser}>修改使用者</Button>
                <Button variant="outlined" onClick={handleCreateStore}>查商品後加入商店</Button>
                <Button variant="outlined" onClick={handleCreateCommClick}>加商品</Button>
                <Button variant="outlined" onClick={HandleTestAPIClick}>TestAPI</Button>
            </Stack>
            <Stack spacing={2} direction="row" classes={classes.basic}>
                <Button variant="outlined" onClick={handleCreateOrderClick}>創建訂單</Button>
                <Button variant="outlined" onClick={handleQueryAllOrderClick}>查詢所有訂單</Button>
                <Button variant="outlined" onClick={handleQueryCurrUserOrderClick}>查詢目前使用者的訂單</Button>
            </Stack>
        </div>
    );
}