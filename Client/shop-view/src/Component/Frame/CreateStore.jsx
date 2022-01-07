import * as React from 'react';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import Stepper from '../Tools/Stepper'
import Button from '@mui/material/Button';
import { makeStyles } from '@material-ui/core';
import TextField from '@mui/material/TextField';
import axios from 'axios';
import { config } from '../../api/config'

//畫面css

const useStyles = makeStyles(theme => ({
    basic: {
        //paddingTop : 300,
        paddingTop: "20px"
    },
    context: {
        paddingTop: "2%",
        paddingLeft: "25%",
        paddingRight: "25%",
    },
    txt: {
        marginTop: "2%"
    },
    divBord: {
        borderColor: '#aaaaee',
        borderWidth: '3px',
        borderStyle: 'solid',
        padding: '5px'
    },
    button: {
        marginTop: "2%"
    }
}));


export default function CreateStore(props) {
    const classes = useStyles();
    const userId = parseInt(localStorage.getItem("userId"));
    const [store, setStore] = React.useState({
        UserId: userId,
        Name: "",
        Classification: "",
        Describe: "",
    })

    const handleCreateStore = () => {
        if (store.Name == "" || store.Classification == "" || store.Describe == "") {
            alert("請輸入完整資料")
            return;
        }

        console.log(store)
        axios.post('api/Store/CreateStore', store, config)
            .then(response => {
                console.log(response)
                axios.get('api/Store/GetStoreByUserId/' + userId, config)
                .then(response => {
                  console.log("storeId", response.data[0].storeId)
                  localStorage.setItem('storeId' , response.data[0].storeId)
                })
                alert(response.data.message);
                //window.close();
            })
    }

    const handleTextChange = event => {
        event.persist();
        switch (event.target.id) {
            case "Name":
                setStore(oldValues => ({
                    ...oldValues,
                    Name: event.target.value
                }))
                break;
            case "Classification":
                setStore(oldValues => ({
                    ...oldValues,
                    Classification: event.target.value
                }))
                break;
            case "Describe":
                setStore(oldValues => ({
                    ...oldValues,
                    Describe: event.target.value
                }))
                break;
        }
    }

    return (
        <div>
            <Box className={classes.context}>
                <h1>創建賣場</h1>
                <TextField id="Name" label="賣場名稱" variant="outlined" fullWidth onChange={handleTextChange} />
                <TextField id="Classification" label="賣場性質分類" variant="outlined" fullWidth style={{ "margin-top": "2%" }} onChange={handleTextChange} />
                <TextField
                    id="Describe"
                    label="賣場描述"
                    variant="outlined"
                    fullWidth={true}
                    multiline={true}
                    rows={7}
                    rowsMax={7}
                    style={{ "margin-top": "3%" }}
                    onChange={handleTextChange}
                />
                <Button
                    variant="contained"
                    color="primary"

                    style={{ "float": "right", "marginTop": "2%" }}
                    onClick={handleCreateStore} >
                    創建商店
                </Button>
            </Box>
        </div >
    );
}