import * as React from 'react';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { makeStyles } from '@material-ui/core';
import { fabClasses } from '@mui/material';
import axios from 'axios';
import { config } from '../../api/config'
import Card from './Card'
import banner from '../image/new year.jpg'

//畫面css

const useStyles = makeStyles(theme => ({
    basic: {
        //paddingTop : 300,
        paddingTop: "20px"
    },
    context: {
        paddingTop: "2%",
        paddingLeft: "12%",
        paddingRight: "12%",
    },
    Card: {
        marginRight: '2%',
        display: "inline-block",
    },
    banner: {
        backgroundColor: 'yellow',
        height: '300px',
        width: '100%',
    },
    SpcText: {

    }
}));

export default function Main() {
    const classes = useStyles();
    var objects = ['', '', '']
    const [commodity, setCommodity] = React.useState({
        Id: 1,
        Name: "滑鼠",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/YDgZneA.jpg",
        StoreId: 1
    })
    const [commodity1, setCommodity1] = React.useState({
        Id: 2,
        Name: "鍵盤",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/J3YKs2y.jpg",
        StoreId: 1
    })
    const [commodity2, setCommodity2] = React.useState({
        Id: 3,
        Name: "顯示卡",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/N8d9itX.jpg",
        StoreId: 1
    })
    const [commodity3, setCommodity3] = React.useState({
        Id: 4,
        Name: "Air Pods",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/jDAL8qO.jpg",
        StoreId: 1
    })
    const [commodity4, setCommodity4] = React.useState({
        Id: 5,
        Name: "USB隨身碟",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/uNm2HkQ.jpg",
        StoreId: 1
    })
    const [commodity5, setCommodity5] = React.useState({
        Id: 6,
        Name: "有線耳機",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/WrhFWFj.jpg",
        StoreId: 1
    })
    const [commodity6, setCommodity6] = React.useState({
        Id: 7,
        Name: "行動電源",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/6C3Gdfz.png",
        StoreId: 1
    })
    const [commodity7, setCommodity7] = React.useState({
        Id: 8,
        Name: "Iphone",
        Classification: "水果",
        Describe: "水果",
        Price: 100,
        ImagePath: "https://i.imgur.com/1VNEG8H.png",
        StoreId: 1
    })

    React.useEffect(() => {
        axios.get('api/Commodity/GetRecommendCard', config)
            .then(response => {
                console.log(response)
                console.log('Run GetRecommendCard')
            })
        // axios.get('api/Commodity/GetDiscountCard', config)
        // .then(response => {
        //     console.log(response)
        // })
    }, [])


    return (
        <div className={classes.basic}>
            <div className={classes.banner}>
                <img className={classes.banner} src="https://localhost:44387/api/User/getImage/banner"></img>
            </div>
            <div className={classes.context}>
                <h1 className={classes.SpcText}>推薦商品</h1>
                <div className={classes.Card}>
                    <Card Data={commodity} />
                </div >
                <div className={classes.Card}>
                    <Card Data={commodity1} />
                </div >
                <div className={classes.Card}>
                    <Card Data={commodity2} />
                </div >
                <Card Data={commodity3} />
            </div>
            <div className={classes.context}>
                <h1 className={classes.SpcText}>優惠商品</h1>
                <div className={classes.Card}>
                    <Card Data={commodity4} />
                </div >
                <div className={classes.Card}>
                    <Card Data={commodity5} />
                </div >
                <div className={classes.Card}>
                    <Card Data={commodity6} />
                </div >
                <Card Data={commodity7} />
            </div>
        </div>
    );
}