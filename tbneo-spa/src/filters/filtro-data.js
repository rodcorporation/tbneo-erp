import moment from 'moment'

const filtroData = (value) => {
    if (value) {
        return moment(String(value)).format('MM/DD/YYYY hh:mm')
    }
};

export { filtroData }