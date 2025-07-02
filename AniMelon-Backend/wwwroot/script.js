const fileInput = document.getElementById('fileInput');
const statusDiv = document.getElementById('status');
const errorDiv = document.getElementById('error');
const player = videojs('videoPlayer', {
    controls: true,
    autoplay: false,
    preload: 'auto',
    liveui: true // Enable live UI for dynamic HLS
});

async function uploadFile() {
    const file = fileInput.files[0];
    if (!file) {
        errorDiv.textContent = 'Please select an MKV file.';
        return;
    }

    statusDiv.textContent = 'Uploading file...';
    errorDiv.textContent = '';

    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await fetch('https://localhost:7082/video/upload', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            throw new Error('Upload failed: ' + (await response.text()));
        }


        const data = await response.json();
        statusDiv.textContent = 'Starting stream...';

        // Poll for the playlist until it's available


        await waitForPlaylist("https://localhost:7082" + data.hlsUrl);

        const subtitleSelect = document.getElementById('subtitle-select');
        if (data.subtitles && data.subtitles.length > 0) {
            data.subtitles.forEach(sub => {
                const option = document.createElement('option');
                option.value = sub.index;
                option.text = `${sub.title} (${sub.language})`;
                subtitleSelect.appendChild(option);
            });
        } else {
            subtitleSelect.innerHTML = '<option value="none">No subtitles available</option>';
        }

        // Set the HLS source for the player
        player.src({
            src: data.hlsUrl,
            type: 'application/x-mpegURL'
        });
        player.play();
    } catch (error) {
        errorDiv.textContent = 'Error: ' + error.message;
        statusDiv.textContent = '';
    }
}

async function waitForPlaylist(hlsUrl, retries = 10, delay = 1000) {
    for (let i = 0; i < retries; i++) {
        try {
            const response = await fetch(hlsUrl);
            if (response.ok) {
                return; // Playlist is available
            }
        } catch {
            // Ignore fetch errors and retry
        }
        await new Promise(resolve => setTimeout(resolve, delay));
    }
    throw new Error('HLS playlist not available after retries.');
}