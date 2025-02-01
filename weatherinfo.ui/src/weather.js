import { Oval } from 'react-loader-spinner';
import React, { useState } from 'react';
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFrown } from '@fortawesome/free-solid-svg-icons';
import './weather.css';

function WeatherApp() {
    const [city, setCity] = useState('');
    const [country, setCountry] = useState('');
    const [weather, setWeather] = useState({
        loading: false,
        data: {},
        error:{},
        hasError: false,
    });
    const search = async (event) => {
        event.preventDefault();
        setWeather({ ...weather, loading: true });

        const url = `https://localhost:44313/Weather/Details?city=${city}&country=${country}`;
        await axios.get(url, {
            headers: {
                'x-api-key': 'dc1436dae7714754b7b1df6d9243048c'
            }
        })
            .then((res) => {
                setWeather({ data: res.data, loading: false, error: {} , hasError:false});
            })
            .catch((error) => {
                console.log("error1", error.response.data)
                debugger
                setWeather({ data: error.response.data, error: error.errors, loading: false, hasError:true });
            });
            console.log("error", weather.error)
            console.log("Data", weather.data)
    };


    return (
        <div className="WeatherCard">
            <h1 className="weather-title">
                Weather Information
            </h1>
            <div className="search">
                <input
                    required
                    type="text"
                    className="city-search"
                    placeholder="Enter City Name.."
                    name="city"
                    value={city}                    
                    onChange={(event) => setCity(event.target.value)}
                />
            </div>
            <br />
            <div className="search-bar">
                <input
                    required
                    type="text"
                    className="country-search"
                    placeholder="Enter Country Name.."
                    name="country"
                    value={country}
                    onChange={(event) => setCountry(event.target.value)}
                />
            </div>
            <br />
            <div className='search-button-div'>
                <button className='search-button' onClick={search}>
                    Get Weather Information
                </button>
            </div>
            {weather.loading && (
                <>
                    <br />
                    <br />
                    <Oval type="Oval" color="black" height={100} width={100} />
                </>
            )}
            {weather.hasError && (
                <>
                    <br />
                    <br />
                    <span className="error-message">
                        <FontAwesomeIcon icon={faFrown} />
                        <span style={{ fontSize: '20px' }}> {weather.data.errorMessage} </span>
                        <span style={{ fontSize: '20px' }}> {weather.error.errors.city[0] + weather.error.errors.country[0] } </span>
                    </span>
                </>
            )}
            {weather && weather.data && (
                <div>
                    <div className="des-wind">
                        <p>{weather.data?.description?.toUpperCase()}</p>
                    </div>
                </div>
            )}
        </div>
    );
}

export default WeatherApp;
