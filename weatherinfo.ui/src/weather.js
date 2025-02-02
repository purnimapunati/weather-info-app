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
        error: {},
        hasError: false,
    });

    const search = async (event) => {
        event.preventDefault();
        setWeather({ ...weather, loading: true });

        const url = `https://localhost:44313/Weather/Details?city=${city}&country=${country}`;

        try {
            const res = await axios.get(url, {
                headers: {
                    'x-api-key': 'dc1436dae7714754b7b1df6d9243048c'
                }
            });

            // If the statusCode is 200, the request was successful
            if (res.data.statusCode === 200) {
                setWeather({
                    loading: false,
                    data: res.data,
                    error: {},
                    hasError: false
                });
            } else {
                // In case the status code is not 200, display an error message
                setWeather({
                    loading: false,
                    data: {},
                    error: { errorMessage: 'Unexpected error occurred' },
                    hasError: true
                });
            }
        } catch (error) {
            console.error("Error fetching weather data:", error);

            // Check if the error response has data or error message
            if (error.response) {
                const status = error.response.status;
                const errorMessage = error.response.data.errorMessage || "An error occurred";

                if (status === 401) {
                    setWeather({
                        loading: false,
                        data: {},
                        error: { errorMessage: 'Invalid API Key or Unauthorized access' },
                        hasError: true
                    });
                } else if (status === 429) {
                    setWeather({
                        loading: false,
                        data: {},
                        error: { errorMessage: 'Rate limit exceeded,maximum admitted 5 per 1h, please try again later' },
                        hasError: true
                    });
                } else {
                    // Other errors like server errors (500)
                    setWeather({
                        loading: false,
                        data: {},
                        error: { errorMessage: errorMessage },
                        hasError: true
                    });
                }
            } else if (error.request) {
                // Network error (request was made, but no response received)
                setWeather({
                    loading: false,
                    data: {},
                    error: { errorMessage: 'Network error, please check your connection' },
                    hasError: true
                });
            } else {
                // Other unknown errors
                setWeather({
                    loading: false,
                    data: {},
                    error: { errorMessage: 'Unknown error occurred' },
                    hasError: true
                });
            }
        }
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
            <div className="search-button-div">
                <button className="search-button" onClick={search}>
                    Get Weather Information
                </button>
            </div>

            {/* Loading Spinner */}
            {weather.loading && (
                <>
                    <br />
                    <br />
                    <Oval type="Oval" color="black" height={100} width={100} />
                </>
            )}

            {/* Error Handling */}
            {weather.hasError && (
                <>
                    <br />
                    <br />
                    <span className="error-message">
                        <FontAwesomeIcon icon={faFrown} />
                        {/* Display errorMessage from response */}
                        {weather.error.errorMessage && (
                            <span style={{ fontSize: '20px' }}>
                                {weather.error.errorMessage}
                            </span>
                        )}
                    </span>
                </>
            )}

            {/* Display Weather Data */}
            {weather.data && weather.data.description && (
                <div>
                    <div className="des-wind">
                        <p>{weather.data.description.toUpperCase()}</p>
                    </div>
                </div>
            )}
        </div>
    );
}

export default WeatherApp;
