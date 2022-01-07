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
import Snackbar from '@mui/material/Snackbar';
import MuiAlert from '@mui/material/Alert';

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
    const [open, setOpen] = React.useState(false);

    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }

        setOpen(false);
    };
    const Alert = React.forwardRef(function Alert(props, ref) {
        return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
    });

    const [commodity, setCommodity] = React.useState({
        Name: "",
        Price: "",
        ImagePath: "",
        Price: 0,
        Count:0,
        Descrite : "",
        CanBuy : [1]
    })

    const [loginState , setLoginState] = React.useState({
        userId: "" , 
        isLogin : false
    })

    let query = useQuery()

    React.useEffect(() => {
        var queryData = {
            StoreId : query.get("StoreId"),
            CommodityId : query.get("CommodityId")
        }
        userCheck()

        axios.post(`api/Store/GetCommodityByStore`, queryData , config)
            .then(response => {
                console.log(response.data)
                var tempCount = []
                var needAdd = 0 
                if(response.data.count > 5){
                    needAdd = 5
                }
                else{
                    needAdd = response.data.count
                }
                for(let i = 1 ; i <= needAdd ; i++){
                    tempCount.push(i)
                }
                
                setCommodity(oldValues => ({
                    ...oldValues,
                    Name: response.data.commodityName,
                    Price : response.data.commodityPrice,
                    Count :response.data.count,
                    ImagePath : response.data.commodityImage,
                    Descrite : response.data.commodityDesc,
                    CanBuy : tempCount
                }));
            })
       
    }, [])

  
    const handleChange = event => {
        setNumber(event.target.value)
    }

    const handleAddCarClick = event => {
        if(loginState.userId != "" && loginState.isLogin){
            
            var order = ({
                UserId : loginState.userId,
                CommmodityId :query.get("CommodityId")
            })
            
            axios.post('api/BuyList/AddBuyList' ,order , config)
            .then(response =>{
                console.log(response)
                alert(response.data.message)
            })
        }
        else{
            setOpen(true);
        }  
    }

    const handleBuyClick = event => {
        console.log(commodity)
        if(loginState.userId != "" && loginState.isLogin){
            var order = ({
                UserId : loginState.userId,
                CommmodityId :query.get("CommodityId")
            })

            axios.post('api/Order/AddOrder' ,order , config)
            .then(response =>{
                console.log(response)
                alert(response.data.message)
            })
        }
        else{
            setOpen(true);
        }  
    }

    const userCheck = () =>{
        var userId = localStorage.getItem("userId")

        if (userId != "" && typeof userId != "undefined") {
           setLoginState(oldValues => ({
               ...oldValues,
               userId : userId,
               isLogin : true
           }))
        }
    }

    const GoodsNumber = [1, 2, 3, 4, 5]

    const [number, setNumber] = React.useState(0);

    return (
        <div className={classes.basic}>
            <div className={classes.context}>
                <h1>{`商品名稱 : ${commodity.Name}`}</h1>
                <div className={classes.commImg}>
                    <div className={classes.contextColor}>
                        <div className={classes.smallBlock}>
                            <p>{`價格 : ${commodity.Price}`}</p>
                            <p>{`現貨數量 : ${commodity.Count}`}</p>
                            <p style={{ 'display': 'inline-block' }}>購買數量 </p>
                            <TextField
                                id="outlined-select-currency"
                                select
                                label="選擇購買數量"
                                value={number}
                                onChange={handleChange}
                                style={{ "width": "30%", "margin-left": "3%" }}
                            //helperText="選擇購買數量"
                            >
                                {commodity.CanBuy.map((option) => (
                                    <MenuItem key={option} value={option}>
                                        {option}
                                    </MenuItem>
                                ))}
                            </TextField>

                            <div>
                                <br />
                                <Button variant="contained" color="secondary" startIcon={<AddShoppingCartIcon />} stlye={{ "margin-top": "2%" }} onClick={handleAddCarClick}>加入購物車</Button>
                                <Button variant="contained" color="secondary" startIcon={<LocalAtmIcon />} style={{ "margin-left": "2%" }} onClick={handleBuyClick}>直接購買</Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div className={classes.commContext} >
                    <img src={commodity.ImagePath} style={{ "width": "300px", "height": "300px" }} />
                </div>
                <br />
                <div className={classes.desc}>
                    <h3>商品描述</h3>
                    <div style={{ "border:": "2px #DCDCDC solid" }}>
                        {commodity.Descrite}
                    </div>
                </div>
            </div>
            <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
                <Alert onClose={handleClose} severity="error" sx={{ width: '100%' }}>
                    請先登入
                </Alert>
            </Snackbar>

        </div>
    );
}