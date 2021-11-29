import * as React from 'react';
import Box from '@mui/material/Box';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { makeStyles } from '@material-ui/core';


const useStyles = makeStyles(theme => ({
    basic: {
        //paddingTop : 300,
        fontSize: "50px"
    },
}));


export default function HorizontalLinearStepper(props) {
    const classes = useStyles();
    const [activeStep, setActiveStep] = React.useState(props.props);
    const [skipped, setSkipped] = React.useState(new Set());
    const [steps, setSteps] = React.useState(['']);
    //只執行一次
    React.useEffect(() => {
        console.log(`stepString = ${props.stepString}`)
        setSteps(props.stepString)
    }, [])

    //每次變換都執行
    React.useEffect(() => {

        setActiveStep(props.props);

    });


    return (
        <Box sx={{ width: '100%' }}  >
            <Stepper activeStep={activeStep} >
                {steps.map((label, index) => {
                    const stepProps = {};
                    const labelProps = {};
                    return (
                        <Step key={label} {...stepProps} >
                            <StepLabel {...labelProps}>{label} </StepLabel>
                        </Step>
                    );
                })}
            </Stepper>
        </Box>
    );
}
