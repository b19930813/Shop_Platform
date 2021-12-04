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
        Price: 3000
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

    const HandleTestAPIClick = () =>{
        axios.post('api/Line', "" , config)
        .then(response => {
            console.log(response.data.message)
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

    return (
        <div className={classes.basic}>
            <Stack spacing={2} direction="row" classes={classes.basic}>
                <Button variant="text" onClick={handleGetUserclick}>查使用者List</Button>
                <Button variant="contained" onClick={handleCreateUserClick}>建立使用者</Button>
                <Button variant="outlined" onClick={handleCreateStore}>查商品後加入商店</Button>
                <Button variant="outlined" onClick={handleCreateCommClick}>加商品</Button>
                <Button variant="outlined" onClick={HandleTestAPIClick}>TestAPI</Button>
            </Stack>
        </div>
    );
}