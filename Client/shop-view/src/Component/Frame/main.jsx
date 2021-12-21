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
    banner:{
        backgroundColor :'yellow',
        height: '300px',
        width: '100%',
    },
    SpcText:{
  
    }
}));

export default function Main() {
    const classes = useStyles();
    var objects = ['', '', '']

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
            <div className = {classes.banner}>
            <img className={classes.banner} src="https://localhost:44387/api/User/getImage/banner"></img>
            </div>
            <div className={classes.context}>
                <h1 className={classes.SpcText}>推薦商品</h1>
                <div className={classes.Card}>
                    <Card />
                </div >
                <div className={classes.Card}>
                    <Card />
                </div >
                <div className={classes.Card}>
                    <Card />
                </div >
                <Card />
            </div>
            <div className={classes.context}>
            <h1 className={classes.SpcText}>優惠商品</h1>
                <div className={classes.Card}>
                    <Card />
                </div >
                <div className={classes.Card}>
                    <Card />
                </div >
                <div className={classes.Card}>
                    <Card />
                </div >
                <Card />
            </div>
        </div>
    );
}