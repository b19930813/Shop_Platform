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
    }
}));


export default function Register() {
    const classes = useStyles();
    const [stepValue, setStepValue] = React.useState(0);
    const [buttonEnable, setButtonEnable] = React.useState({
        prev: false,
        next: false
    })

    const [user, setUser] = React.useState({
        Account: "",
        Password: "",
        Name: "",
        Phone: "",
        Address: "",
        LineID: ""
    });

    const [confirmPassword, setConfirmPassword] = React.useState("");

    React.useEffect(() => {
        if (stepValue == 0) {
            setButtonEnable({ prev: true })
        }
    }, [])

    //回傳表單
    let StepForm = (index) => {
        var view = "";
        switch (index) {
            case 0:
                view = <Box className={classes.context}>
                    <TextField id="Account" required label="帳號" variant="outlined" helperText="請輸入Email" fullWidth value={user.Account} onChange={handleTextChange} onBlur = {handleDataComfirm} />
                    <TextField id="Password" required label="密碼" variant="outlined" type="password" helperText="密碼長度必須大於8個字元，區分大小寫" fullWidth value={user.Password} style={{ "margin-top": "2%" }} onChange={handleTextChange} />
                    <TextField id="ComformPassword" required label="確認密碼" variant="outlined" type="password" helperText="再次輸入密碼做確認" fullWidth value={confirmPassword} style={{ "margin-top": "2%" }} onChange={handleTextChange} />
                    <TextField id="Name" label="姓名" variant="outlined" fullWidth style={{ "margin-top": "2%" }} value={user.Name} onChange={handleTextChange} />
                    <TextField id="Phone" label="電話" variant="outlined" fullWidth style={{ "margin-top": "2%" }} value={user.Phone} onChange={handleTextChange} />
                    <TextField id="Address" label="地址" variant="outlined" fullWidth style={{ "margin-top": "2%" }} value={user.Address} onChange={handleTextChange} />
                </Box>
                break;
            case 1:
                view = <Box className={classes.context}>
                    <TextField id="Line ID" label="Line的ID" variant="outlined" value={user.LineID} helperText="在Line機器人上輸入「取得資訊」獲得ID" fullWidth className={classes.txt} onChange={handleTextChange} />
                </Box>
                break;
            case 2:
                view = <Box className={classes.context}>
                    <div className={classes.divBord}>
                        <p> Account : {user.Account} </p>
                        <p> Name : {user.Name} </p>
                        <p> Phone : {user.Phone} </p>
                        <p> Address : {user.Address} </p>
                        <p> Line ID : {user.LineID} </p>
                    </div>
                </Box>
                break;
            case 3:
                view = <Box className={classes.context}>
                    <p>註冊成功!</p>
                </Box>
                break;
            default:
                break;
        }
        return view
    }

    const handleValuePlus = () => {
        if (stepValue == 0) {
            //確認資料都有填入
            if (user.Account == '' || user.Password == '' || confirmPassword == '' || user.Name == '') {
                alert("資料請完整填寫");
                return;
            }
            if (user.Password != confirmPassword) {
                alert("密碼跟確認密碼不一樣");
                return;
            }
        }
        setStepValue(stepValue + 1)
        if (stepValue == 2) {
            setButtonEnable(oldValues => ({
                ...oldValues,
                next: true
            }));
            //發送API處理結果 
            axios.post('api/User/Register', user, config)
                .then(response => {
                   
                })
                setTimeout("document.location.href = '/'",2000);
        }
        setButtonEnable(oldValues => ({
            ...oldValues,
            prev: false
        }));

        //Data Check
    }
    const handleValuenelf = () => {

        setStepValue(stepValue - 1)
        if (stepValue == 1) {
            setButtonEnable(oldValues => ({
                ...oldValues,
                prev: true
            }));
        }
        setButtonEnable(oldValues => ({
            ...oldValues,
            next: false
        }));

    }

    const handleDataComfirm = event =>{
        console.log('test')
    }

    const handleTextChange = event => {
        event.persist();
        switch (event.target.id) {
            case "Account":
                setUser(oldValues => ({
                    ...oldValues,
                    Account: event.target.value
                }));
                break;
            case 'Password':
                setUser(oldValues => ({
                    ...oldValues,
                    Password: event.target.value
                }));
                console.log('Password')
                break;
            case 'ComformPassword':
                setConfirmPassword(event.target.value);
                break;
            case 'Name':
                setUser(oldValues => ({
                    ...oldValues,
                    Name: event.target.value
                }));
                break;
            case 'Phone':
                setUser(oldValues => ({
                    ...oldValues,
                    Phone: event.target.value
                }));
                break;
            case 'Address':
                setUser(oldValues => ({
                    ...oldValues,
                    Address: event.target.value
                }));
                break;
            case 'Line ID':
                setUser(oldValues => ({
                    ...oldValues,
                    LineID: event.target.value
                }));
                break;
            default:
                break;
        }
    }

    return (
        <React.Fragment>
            <CssBaseline />
            <Container fixed className={classes.basic} >
                <Stepper props={stepValue} stepString={['使用者註冊', 'Line Bot設定', '確認資料', '完成註冊']} />
                {StepForm(stepValue)}
                <Box className={classes.context}>
                    <Button variant="contained" onClick={handleValuenelf} disabled={buttonEnable.prev} >上一步</Button>
                    <Button variant="contained" onClick={handleValuePlus} style={{ 'float': 'right' }} disabled={buttonEnable.next}>下一步</Button>
                </Box>
            </Container>
        </React.Fragment>
    );
}