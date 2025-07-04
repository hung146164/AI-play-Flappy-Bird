# 🐤 AI-play-Flappy-Bird

An AI-powered Flappy Bird game built with **Unity** and **ML-Agents**, where the agent learns to play using **reinforcement learning**. The AI learns to survive by avoiding pipes and maximizing its score using the **PPO (Proximal Policy Optimization)** algorithm.

---

## 🎮 Demo

> After training, the AI agent can pass through more than 100 pipes consecutively.

> Video Demo: https://youtu.be/iHn-Z4ikRyo

---

> You can download it from: https://drive.google.com/drive/u/2/folders/1m6XR2w5FFZsKn3ouMKKbLHMg8o3iEzOP

## 📁 Project Structure
AI-play-Flappy-Bird/
├── Assets/ # Unity project files
├── config/ # ML-Agents configuration files
├── results/ # Training logs and TensorBoard data
│ └── flappy-colab-Master_Model/
│  ├── events.out.tfevents... # TensorBoard logs
│  └── run_tensorboard.bat # One-click TensorBoard launcher
├── TrainGGColabFile/ # Google Colab notebook for training
├── README.md


---

## 🧠 Technologies Used

| Component       | Technology         |
|----------------|--------------------|
| Game Engine     | Unity              |
| AI Framework    | ML-Agents          |
| Training Algo   | PPO (Reinforcement Learning) |
| Visualization   | TensorBoard        |
| Training Env    | Google Colab       |

---

## 🚀 How to Run the AI Agent

### 1. Clone the Repository

```bash
git clone https://github.com/<your-username>/AI-play-Flappy-Bird.git
```
2. Open the Project with Unity
Recommended Unity version: 2022.x or newer

Open the main scene

Press Play to see the trained AI in action

📊 View Training Results with TensorBoard
To visualize the AI training performance:

Navigate to:

```bash
results/flappy-colab-Master_Model/
```
Double-click the file:
```bash
run_tensorboard.bat
```
Open your browser and go to:
```bash
http://localhost:6006
```
⚠️ If TensorBoard is not installed, run:
``` bash
pip install tensorboard
```
🧪 Retrain the AI from Scratch (Optional)
To retrain the model:

Open the Colab notebook:

```bash
TrainGGColabFile
```
📤 Upload Files for Training on Google Colab
There are two training modes, and each requires different files to be uploaded:

1. 🔁 New Training Session
To train from scratch, make sure to upload:

✅ The Linux build of your Unity environment (.x86_64 file)

✅ The corresponding YAML configuration file (.yaml)

2. 🔄 Resume a Previous Training Session
To continue training from where you left off, upload:

✅ The ZIP file downloaded at the end of the previous training
(It contains saved model checkpoints, optimizer states, and training metadata)

💡 After uploading the ZIP file, run all to continue train.

📌 Notes
The goal is to teach AI to play a simple game through rewards and exploration.

The model improves over time and learns to survive longer with more consistent rewards.

Easily extendable to other games (Dino Game, Pong, Snake, etc.).

🙋 Author
Name: Hung Nguyen

Major: Game developer

Contact: https://web.facebook.com/hung2075/

✨ Future Improvements
Add human vs AI mode

Experiment with other algorithms (SAC, DQN, etc.)

Deploy on WebGL for live demo online

