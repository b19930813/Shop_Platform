import React from 'react';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import axios from 'axios'
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import { config } from '../../api/config'

const useStyles = makeStyles(theme => ({
  form: {
    width: '100%',
    marginTop: theme.spacing(3),

  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  // email: {
  //   disabled: true,
  //   width: "500px",
  // },
  // password: {
  //   width: "500px",
  // },
  // h1: {
  //   marginTop: theme.spacing(3),
  //   width: '100%',
  // }
}));

export default function LoginForm() {
  const classes = useStyles();


  const [loginData, setLoginData] = React.useState({
    account: "",
    password: ""
  })



  const handleTextChange = event => {

    switch (event.target.id) {
      case "account":
        setLoginData(oldValues => ({
          ...oldValues,
          account: event.target.value
        }));
        break;
      case "password":
        setLoginData(oldValues => ({
          ...oldValues,
          password: event.target.value
        }));
        break;
    }
  }

  const handleLogin = event => {
    event.preventDefault();
    axios
      .post('/api/User/Login', loginData, config)
      .then(response => {
        //if(response.data != "Fail"){
        if (response.data.status != "401") {
          console.log("userId", response.data.message.userId)
          localStorage.setItem('userId', response.data.message.userId)
          localStorage.setItem('userName', response.data.message.name)

          axios.get('api/Store/GetStoreByUserId/' + response.data.message.userId, config)
            .then(response => {
              console.log("storeId", response.data[0].storeId)
              localStorage.setItem('storeId' , response.data[0].storeId)
            })

          alert('登入成功');
          window.location.reload();
        }
        else {
          alert('登入失敗，請確認帳號密碼是否正確');
        }
      })
      .catch(exception => {
        alert('登入失敗')
      })
  }

  return (
    <div>
      <form className={classes.form} noValidate>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <TextField
              variant="outlined"
              required
              fullWidth
              id="account"
              label="account"
              name="account"
              autoComplete="account"
              onChange={handleTextChange}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              variant="outlined"
              required
              fullWidth
              id="password"
              name="password"
              label="密碼"
              type="password"
              autoComplete="current-password"
              onChange={handleTextChange}
            />
          </Grid>
          <Grid item xs={12}>
            {/* <FormControlLabel
                    control={<Checkbox value="allowExtraEmails" color="primary" />}
                    label="記住我(尚未)"
                    onChange = {() => console.log("run onChange")}
                  /> */}
          </Grid>
        </Grid>
        <Button
          type="submit"
          fullWidth
          variant="contained"
          color="primary"
          className={classes.submit}
          onClick={handleLogin}
        >

          登入
        </Button>
        {/* <Grid container justify="flex-end">
                <Grid item>
                  <Link href="#" variant="body2">
                    忘記密碼了?(尚未)
                </Link>
                </Grid>
              </Grid> */}
      </form>
    </div>
  );
}