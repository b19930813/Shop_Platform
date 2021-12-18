import React from 'react';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import axios from 'axios'
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


    return (
        <div>
   <p>測試</p>
        </div>
    );
}