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


export default function AddCommodity(props) {
    const classes = useStyles();
    const inputFile = React.useRef(null);
    const userName = localStorage.getItem("userName");
    const storeId = parseInt(localStorage.getItem("storeId"));
    const [commodity, setCommodity] = React.useState({
        StoreId: storeId,
        Name: "",
        Classification: "",
        Describe: "",
        Price: 0,
        ImagePath: "",
    })

    const [imageName, setImageName] = React.useState("");
    const handleAddCommodity = () => {
        if (commodity.Name == "" || commodity.Classification == "" || commodity.Describe == "" || imageName == "") {
            alert("請輸入完整資料")
            return;
        }

        console.log(commodity)
        axios.post('api/Commodity/AddCommodity', commodity, config)
            .then(response => {
                console.log(response)
                alert(response.data.message);
                window.close();
            })
    }

    const handleFileUpload = e => {
        const { files } = e.target;
        if (files && files.length) {
            const filename = files[0].name;

            var parts = filename.split(".");
            const fileType = parts[parts.length - 1];
            // console.log("fileType", fileType); //ex: zip, rar, jpg, svg etc.
            // console.log(files[0])
            setCommodity(oldValues => ({
                ...oldValues,
                //ImagePath: files
                ImagePath: "https://i.imgur.com/" + files[0].name
                //ImagePath: files[0].name.substring(0, files[0].name.indexOf("."))
            }))
            setImageName(files[0].name)
        }
    };

    const handleTextChange = event => {
        event.persist();
        switch (event.target.id) {
            case "Name":
                setCommodity(oldValues => ({
                    ...oldValues,
                    Name: event.target.value
                }))
                break;
            case "Classification":
                setCommodity(oldValues => ({
                    ...oldValues,
                    Classification: event.target.value
                }))
                break;
            case "Describe":
                setCommodity(oldValues => ({
                    ...oldValues,
                    Describe: event.target.value
                }))
                break;
            case "Price":
                setCommodity(oldValues => ({
                    ...oldValues,
                    Price: parseInt(event.target.value)
                }))
                break;
            case "ImagePath":
                setCommodity(oldValues => ({
                    ...oldValues,
                    ImagePath: event.target.value
                }))
                break;
        }
    }

    const handleImageSelect = () => {
        inputFile.current.click();
    }

    return (
        <div>
            <Box className={classes.context}>
                <h1>加入商品 - {`${userName}的商店`}</h1>
                <TextField id="Name" label="商品名稱" variant="outlined" fullWidth onChange={handleTextChange} />
                <TextField id="Classification" label="商品分類" variant="outlined" fullWidth style={{ "margin-top": "2%" }} onChange={handleTextChange} />
                <TextField id="Price" label="價格" variant="outlined" fullWidth style={{ "margin-top": "2%" }} onChange={handleTextChange} />
                <TextField id="ImagePath" label="圖片" variant="outlined" fullWidth style={{ "margin-top": "2%" }} value={imageName} onChange={handleTextChange} />
                <input
                    style={{ display: "none" }}
                    // accept=".zip,.rar"
                    ref={inputFile}
                    onChange={handleFileUpload}
                    type="file"
                />
                <Button
                    variant="contained"
                    color="secondary"

                    style={{ "float": "right", "marginTop": "2%" }}
                    onClick={handleImageSelect} >
                    選取照片
                </Button>
                <TextField
                    id="Describe"
                    label="商品描述"
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
                    onClick={handleAddCommodity} >
                    加入商品
                </Button>
            </Box>
        </div >
    );
}