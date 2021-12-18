import * as React from 'react';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { makeStyles } from '@material-ui/core';
import { fabClasses } from '@mui/material';
import axios from 'axios';
import { config } from '../../api/config';
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';

//畫面css

const useStyles = makeStyles(theme => ({
    basic: {
        //paddingTop : 300,
        paddingTop: "5px"
    },
    context: {
        paddingTop: "1%",
        paddingLeft: "12%",
        paddingRight: "12%",
    },
    commImg: {
        width: "65%",
        lineHeight: "50px",
        paddingBottom: "45%",
        border: "2px #DCDCDC solid ",
        marginRight: "10px",
        float: 'left',

    },
    commContext: {
        width: "30%",
        lineHeight: "50px",
        paddingBottom: "45%",
        border: "2px #DCDCDC solid",
        float: 'left'
    },
    contextColor:{
        width: "90%",
        border: "2px #DCDCDC solid",
        marginLeft: "5%",
        marginTop: "5%",
        paddingBottom: "20%",
        backgroundColor: "#6495ED",
        borderRadius: "20px"
    },
    smallBlock:{
        color: '#FFF8DC',
        marginLeft: "5%"
    }
}));

export default function Commodity(props) {
    const classes = useStyles();
    return (
        <div className={classes.basic}>
            <div className={classes.context}>
                <h1>商品名稱</h1>
                <div className={classes.commImg}>
                    <div className={classes.contextColor}>
                        <div className={classes.smallBlock}>
                            <p>{`價格 : ${props}`}</p>
                            <p>數量</p>
                            <Button variant="contained" color="secondary" startIcon={<AddShoppingCartIcon/>}>加入購物車</Button>
                        </div>
                    </div>
                </div>
                <div className={classes.commContext}>

                </div>
            </div>
        </div>
    );
}