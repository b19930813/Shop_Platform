import * as React from 'react';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { makeStyles } from '@material-ui/core';
import { fabClasses } from '@mui/material';
import axios from 'axios';
import { config } from '../../api/config';
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';
import LocalAtmIcon from '@mui/icons-material/LocalAtm';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import useQuery from './useQuery'

//畫面css

const useStyles = makeStyles(theme => ({
    basic: {
        //paddingTop : 300,
        paddingTop: "5px",

    },
    context: {
        paddingTop: "1%",
        paddingLeft: "12%",
        paddingRight: "12%",
    },
    commImg: {
        width: "65%",
        //lineHeight: "50px",
        paddingBottom: "100px",
        border: "2px #DCDCDC solid ",
        marginRight: "10px",
        float: 'left',
        display: 'inline',
    },
    commContext: {
        width: "30%",
        //lineHeight: "50px",
        paddingBottom: "100px",
        border: "2px #DCDCDC solid",
        float: 'left',
        display: 'inline',
    },
    contextColor: {
        width: "90%",
        border: "2px #DCDCDC solid",
        marginLeft: "5%",
        marginTop: "5%",
        paddingBottom: "12%",
        backgroundColor: "#6495ED",
        borderRadius: "20px"
    },
    smallBlock: {
        color: '#FFF8DC',
        marginLeft: "5%"
    },
    desc: {
        marginTop: '36%',
    }
}));

export default function Commodity(props) {
    const classes = useStyles();

    const [commodity, setCommodity] = React.useState({
        name : "",
        price : "",
        imagePath : ""
    })

    let query = useQuery()
    React.useEffect(() => {
        axios.get(`api/Commodity/${query.get("id")}`, config)
            .then(response => {
                setCommodity(response)
            })
    }, [])


    const handleChange = event => {
        setNumber(event.target.value)
    }

    const handleAddCarClick = event => {
        console.log("test")
        console.log(commodity);
    }

    const handleBuyClick = event => {
        console.log("handleBuyClick")
    }

    const GoodsNumber = [1, 2, 3, 4, 5]

    const [number, setNumber] = React.useState(0);

    return (
        <div className={classes.basic}>
            <div className={classes.context}>
                <h1>{`商品名稱 : ${commodity.name}`}</h1>
                <div className={classes.commImg}>
                    <div className={classes.contextColor}>
                        <div className={classes.smallBlock}>
                            <p>{`價格 : ${props}`}</p>

                            <p style={{ 'display': 'inline-block' }}>數量 </p>
                            <TextField
                                id="outlined-select-currency"
                                select
                                label="選擇購買數量"
                                value={number}
                                onChange={handleChange}
                                style={{ "width": "30%", "margin-left": "3%" }}
                            //helperText="選擇購買數量"
                            >
                                {GoodsNumber.map((option) => (
                                    <MenuItem key={option} value={option}>
                                        {option}
                                    </MenuItem>
                                ))}
                            </TextField>

                            <div>
                                <br />
                                <Button variant="contained" color="secondary" startIcon={<AddShoppingCartIcon />} stlye={{ "margin-top": "2%" }} onclick={handleAddCarClick}>加入購物車</Button>
                                <Button variant="contained" color="secondary" startIcon={<LocalAtmIcon />} style={{ "margin-left": "2%" }} onclick={handleBuyClick}>直接購買</Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div className={classes.commContext} >
                    <img src="https://localhost:44387/api/User/getImage/1" style={{ "width": "300px", "height": "300px" }} />
                </div>
                <br />
                <div className={classes.desc}>
                    <h3>商品描述</h3>
                    <div style={{ "border:": "2px #DCDCDC solid" }}>
                        test
                    </div>
                </div>
            </div>

        </div>
    );
}